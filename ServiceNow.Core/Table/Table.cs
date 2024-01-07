
using Microsoft.Extensions.Logging;
using SNow.Core.Extensions;
using SNow.Core.Models;
using SNow.Core.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ServiceNow.Core.Test")]
namespace SNow.Core
{
    ///<inheritdoc/>
    public class Table : TableBase, ITable
    {
        ///<inheritdoc/>
        public Table(IServiceNow serviceNow, string tableName, ILogger logger) : base(serviceNow, tableName, logger)
        {
        }

        /// <inheritdoc/>
        public async Task<JsonElement> GetByIdAsync(Guid id)
        {

            var url = $"{SN.BaseAddress}/table/{_tableName}/{id:N}";
            return await _httpClient.GetActionResultAsync<JsonElement>(url, SN.AuthenticateAsync, _logger);

        }

        /// <inheritdoc/>
        public ITable Limit(int limit)
        {
            _pageSize = limit;
            return this;
        }

        /// <inheritdoc/>
        public ITable OrderBy(string orderBy)
        {
            _order += string.IsNullOrEmpty(_order) ? "ORDERBY" + orderBy : "^ORDERBY" + orderBy;
            return this;
        }

        /// <inheritdoc/>
        public ITable OrderByDesc(string orderByDesc)
        {
            _order += string.IsNullOrEmpty(_order) ? "ORDERBYDESC" + orderByDesc : "^ORDERBYDESC" + orderByDesc;
            return this;
        }

        /// <inheritdoc/>
        public ITable Select(string[] fields)
        {
            _select = fields.ToList();
            return this;
        }

        /// <inheritdoc/>
        public ITable SetHeaders(List<KeyValuePair<string, string>> entries)
        {
            foreach (var entry in entries)
            {
                _httpClient.DefaultRequestHeaders.Add(entry.Key, entry.Value);
            }
            return this;
        }

        /// <inheritdoc/>
        public async Task<JsonElement> UpdateAsync(Guid id, object data, bool excludeReferenceLinks = true)
        {
            var excludeLinks = excludeReferenceLinks ? "?sysparm_exclude_reference_link=true" : "";
            var url = $"{SN.BaseAddress}/table/{_tableName}/{id:N}{excludeLinks}";
            var result = await _httpClient.PutActionResultAsync<JsonElement>(url, data, SN.AuthenticateAsync);

            return result;
        }

        ///<inheritdoc/>
        public ITable WithQuery(string query)
        {
            _query = Query.Parse(query);
            _currentPage = 0;
            return this;
        }

        /// <inheritdoc/>
        public async Task<List<JsonElement>> ToListAsync(int? page = null)
        {
            var url = page == null ? RequestGetUrl : RequestUrl + $"&sysparm_offset={page * _pageSize}";
            _currentPage++;
            var result = await _httpClient.GetActionResultAsync<List<JsonElement>>(url, SN.AuthenticateAsync, _logger);

            if (result.Count == 0)
                _currentPage = 0;

            return result;
        }

        /// <inheritdoc/>
        public async Task<JsonElement> CreateAsync(object model)
        {
            var url = $"{SN.BaseAddress}/table/{_tableName}";

            var result = await _httpClient.PostActionResultAsync<JsonElement>(url, model, SN.AuthenticateAsync);
            return result;
        }

        /// <inheritdoc/>
        public async Task<List<JsonElement>> AllToListAsync()
        {
            var page = 0;
            var pagedData = await ToListAsync(page++);

            List<JsonElement> data = new List<JsonElement>();
            while (pagedData.Count > 0)
            {
                try
                {
                    if(_logger != null)
                        _logger.LogInformation("{n} Paged{t}s: {time}", pagedData.Count, typeof(JsonElement).Name, DateTimeOffset.Now);
                    data.AddRange(pagedData);

                    pagedData = await ToListAsync(page++);
                }
                catch (Exception ex)
                {
                    if (_logger != null)
                    {
                        _logger.LogError("Error while retrieving {T}, {message} : {stack}", typeof(JsonElement).Name, ex.Message, ex.StackTrace);
                        _logger.LogError("Ignoring page {p}...", RequestUrl);
                    }
                }
            }
            return data;
        }
    }
    /// <inheritdoc/>
    public class Table<T> : TableBase, ITable<T> where T : ServiceNowBaseModel
    {
        private readonly List<(string PropName, string AttName)> _props = new List<(string PropName, string AttName)>();
        /// <summary>
        /// 
        /// </summary>
        public string _where;
        private readonly bool _withFilterAttribute = false;

        /// <inheritdoc/>
        public Table(IServiceNow serviceNow, string tableName, ILogger logger = null) : base(serviceNow, tableName, logger)
        {
            _select = ClassReflections.GetPropertieNamesInJsonFormat<T>();
            _tableName = tableName;
            _props = ClassReflections.GetJsonPropertyNameData<T>();
            #region Filter
            var attrs = typeof(T).GetCustomAttributes(typeof(SnowFilterAttribute), true);

            if (attrs.Any())
                _withFilterAttribute = true;

            foreach (object attr in attrs)
            {
                var snowFilterAttr = attr as SnowFilterAttribute;
                _query = snowFilterAttr.Query;
            }
            #endregion
        }

        /// <inheritdoc/>
        public Table(IServiceNow serviceNow, ILogger logger = null) : base(serviceNow, "", logger)
        {
            _select = ClassReflections.GetPropertieNamesInJsonFormat<T>();
            _props = ClassReflections.GetJsonPropertyNameData<T>();

            #region SNowTable
            var attrs = typeof(T).GetCustomAttributes(typeof(SnowTableAttribute), true);

            if (attrs.Length < 1)
                throw new ArgumentNullException("SnowTable", "The Class is missing SnowTable Attribute");

            foreach (object attr in attrs)
            {
                var tableNameAttr = attr as SnowTableAttribute;
                if (tableNameAttr != null)
                {
                    if (string.IsNullOrEmpty(tableNameAttr.Name))
                        throw new ArgumentNullException("SnowTable", "The Class is missing SnowTable Attribute");
                }

                _tableName = tableNameAttr.Name;
            }
            #endregion

            #region Filter
            attrs = typeof(T).GetCustomAttributes(typeof(SnowFilterAttribute), true);

            if (attrs.Any())
                _withFilterAttribute = true;

            foreach (object attr in attrs)
            {
                var snowFilterAttr = attr as SnowFilterAttribute;             
                _query = snowFilterAttr.Query;
            }
            #endregion

        }


        ///<inheritdoc/>
        public ITable<T> Limit(int limit)
        {
            _pageSize = limit;
            return this;
        }

        ///<inheritdoc/>
        public ITable<T> OrderBy(Expression<Func<T, object>> expression)
        {
            var propName = (expression.Body as MemberExpression ??
                     ((UnaryExpression)expression.Body).Operand as MemberExpression).Member.Name;
            var attName = _props.Find(p => p.PropName == propName).AttName;
            _order += string.IsNullOrEmpty(_order) ? "ORDERBY" + (attName ?? propName.ToLower()) : "^ORDERBY" + (attName ?? propName.ToLower());
            return this;
        }

        ///<inheritdoc/>
        public ITable<T> OrderByDesc(Expression<Func<T, object>> expression)
        {
            var propName = (expression.Body as MemberExpression ??
                   ((UnaryExpression)expression.Body).Operand as MemberExpression).Member.Name;
            var attName = _props.Find(p => p.PropName == propName).AttName;
            _order += string.IsNullOrEmpty(_order) ? "ORDERBYDESC" + (attName ?? propName.ToLower()) : "^ORDERBYDESC" + (attName ?? propName.ToLower());
            return this;
        }

        ///<inheritdoc/>
        public ITable<T> Select(params Expression<Func<T, object>>[] expressions)
        {
            _select = new List<string>();
            foreach (var propertyLambda in expressions)
            {
                var propName = (propertyLambda.Body as MemberExpression ??
                    ((UnaryExpression)propertyLambda.Body).Operand as MemberExpression).Member.Name;
                var attName = _props.Find(p => p.PropName == propName).AttName;
                _select.Add(attName ?? propName.ToLower());
            }
            return this;
        }

        ///<inheritdoc/>
        public ITable<T> Where(Expression<Func<T, bool>> expr)
        {
            var filterquery = "";
            if (_withFilterAttribute)
                filterquery = _query;// throw new InvalidOperationException("Query set with SNow Filter Attribute, change the query is not allowed!");

            var visitor = new PrintingVisitor<T>(expr);
            visitor.Visit(expr);
            _query = visitor.query;
            _query = _query.Replace("Convert(","").Replace("(", "").Replace(")", "");

            if (_withFilterAttribute)
                _query += "^" + filterquery;

            _query = Query.Parse(_query);
            _currentPage = 0;
            return this;
        }

        ///<inheritdoc/>
        public ITable<T> SetHeaders(List<KeyValuePair<string, string>> entries)
        {
            foreach (var entry in entries)
            {
                _httpClient.DefaultRequestHeaders.Add(entry.Key, entry.Value);
            }
            return this;
        }

        /// <inheritdoc/>
        public async Task<List<T>> ToListAsync(int? page)
        {
            if (SN.Token == null && SN.BasicAuthParams == null)
                await SN.AuthenticateAsync();
            if (_httpClient.DefaultRequestHeaders.Authorization == null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SN.Token);
            var url = page == null ? RequestGetUrl : RequestUrl + $"&sysparm_offset={page * _pageSize}";
            var result = await _httpClient.GetActionResultAsync<List<T>>(url, SN.AuthenticateAsync, _logger);
            _currentPage++;

            if (result?.Count == 0)
                _currentPage = 0;

            return result;
        }

        async Task<T> ITable<T>.GetByIdAsync(Guid id)
        {
            var url = $"{SN.BaseAddress}/table/{_tableName}/{id:N}";
            return await _httpClient.GetActionResultAsync<T>(url, SN.AuthenticateAsync, _logger);
        }

        async Task<bool> ITable<T>.DeleteAsync(Guid id)
        {
            return await DeleteAsync(id);
        }

        async Task<T> ITable<T>.CreateAsync(object model)
        {
            var url = $"{SN.BaseAddress}/table/{_tableName}";

            var result = await _httpClient.PostActionResultAsync<T>(url, model, SN.AuthenticateAsync);
            return result;
        }

        async Task<T> ITable<T>.UpdateAsync(Guid? id, object data, bool excludeReferenceLinks)
        {
            var excludeLinks = excludeReferenceLinks ? "?sysparm_exclude_reference_link=true" : "";
            var url = $"{SN.BaseAddress}/table/{_tableName}/{id:N}{excludeLinks}";

            var result = await _httpClient.PutActionResultAsync<T>(url, data, SN.AuthenticateAsync);

            return result;
        }

        /// <inheritdoc/>
        public async Task<List<T>> AllToListAsync()
        {
            var page = 0;
            var pagedData = await ToListAsync(page++);

            List<T> data = new List<T>();
            while (pagedData.Count > 0)
            {
                try
                {
                    _logger.LogInformation("{n} Paged{t}s: {time}", pagedData.Count, typeof(T).Name, DateTimeOffset.Now);
                    data.AddRange(pagedData);

                    pagedData = await ToListAsync(page++);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error while retrieving {T}, {message} : {stack}", typeof(T).Name, ex.Message, ex.StackTrace);
                    _logger.LogError("Ignoring page {p}...", RequestUrl);
                }
            }
            return data;

        }
    }

    /// <summary>
    /// Base class to share props and methods used in typed and non typed table classes
    /// </summary>
    public class TableBase
    {
        /// <summary>
        /// ServiceNow Instance common to all table  
        /// </summary>
        public IServiceNow SN { get; set; }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected HttpClient _httpClient;
        protected readonly ILogger _logger;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


        /// <summary>
        /// Used when we don't want to pass the sysparm_offset parameter
        /// </summary>
        public string RequestUrl => RequestURI();
        /// <summary>
        /// Used when we want to pass the sysparm_offset parameter,
        /// should be called only in GET Requests
        /// </summary>
        protected string RequestGetUrl => RequestURI(true);

        //set internal to allow tests
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected string _query;
        protected int? _pageSize = 10000;
        protected string _tableName;
        protected string _order = "";
        protected List<string> _select;
        protected int _currentPage = 0;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Used by typed and untyped Table
        /// </summary>
        /// <param name="serviceNow"></param>
        /// <param name="tableName"></param>
        /// <param name="logger"></param>
        public TableBase(IServiceNow serviceNow, string tableName, ILogger logger)
        {
            if (logger != null)
                _logger = logger;

            SN = serviceNow;
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _tableName = tableName;
            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _httpClient.DefaultRequestHeaders.Connection.ParseAdd("keep-alive");
            if(SN.BasicAuthParams != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", SN.BasicAuthParams);
        }

        //those helpers avoid duplicated code between typed and non typed version
        #region Help Methods
        /// <summary>
        /// set internal token prop and update internal HttpClient headers
        /// </summary>
        /// <returns></returns>


        protected string RequestURI(bool withCurrentPage = false)
        {
            List<string> allArgs = new List<string>();
            var mainUri = $"{SN.BaseAddress}/table/{_tableName}?";
            if (_select != null)
            {
                var selectedFields = "sysparm_fields=" + string.Join(",", _select);
                allArgs.Add(selectedFields);
            }

            if (_pageSize != null)
                allArgs.Add($"sysparm_limit={_pageSize}");

            var query = "sysparm_query=";
            if (_query != null)
                query = query.Length > "sysparm_query=".Length ? '^' + _query : query + _query;
            if (!string.IsNullOrEmpty(_order))
                query = query.Length > "sysparm_query=".Length ? '^' + _order : query + _order;

            if (query.Length > "sysparm_query=".Length)
            {
                var splitted = query.Split(new string[] { "=", "!=", "^", "^OR", "OR", "LIKE", ">", ">=", "<", "<=", "ISNOTEMPTY", "ISEMPTY", "STARTSWITH", "ENDSWITH", "NOT LIKE"  }, StringSplitOptions.None);
                for (int i = 0; i < splitted.Length; i++)
                {
                    //Escape parameters.
                    if(i > 0 && i % 2 == 0)
                    {
                        query = query.Replace(splitted[i], Uri.EscapeDataString(splitted[i]));
                    }
                    //Fix Negates
                    if (_select.Any(p => "Not" + p == splitted[i])){
                        var prop = splitted[i];
                        var value = splitted[i + 1];
                
                        query = NegateQuery(prop, value, query);
                    }
                }
                allArgs.Add(query);
            }

            if (withCurrentPage && _pageSize != null)
                allArgs.Add($"sysparm_offset={_currentPage * _pageSize}");

            allArgs.Add("sysparm_exclude_reference_link=true");


            var escapedArgs = allArgs;

            return $"{mainUri}{string.Join("&", escapedArgs)}";
        }

        private string NegateQuery(string prop, string value, string query)
        {
            string pattern = $@"(?<={prop})(.*)(?={value})";
            var _operator = (Regex.Match(query, pattern)).Value;
            Console.WriteLine($"Should negate between {prop} and {value} with operator: {_operator} ");

            var negatedOperator = "";
            
            switch (_operator)
            {
                case "=":
                    negatedOperator = "!=";
                    break;
                case ">=":
                    negatedOperator = "<";
                    break;
                case ">":
                    negatedOperator = "<=";
                    break;
                case "!=":
                    negatedOperator = "=";
                    break;
                case "ISEMPTY":
                    negatedOperator = "ISNOTEMPTY";
                    break;
                case "LIKE":
                    negatedOperator = "NOT LIKE";
                    break;
                case "NOT LIKE":
                    negatedOperator = "LIKE";
                    break;
            }

            return query.Replace($"{prop}{_operator}{value}", $"{prop.Substring("Not".Length)}{negatedOperator}{value}");
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(Guid id)
        {
            if (SN.Token == null)
                await SN.AuthenticateAsync();
            var url = $"{SN.BaseAddress}/table/{_tableName}/{id:N}";

            var result = await _httpClient.DeleteAsync(url);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                await SN.AuthenticateAsync();
                result = await _httpClient.DeleteAsync(url);
            }
            else if (result.IsSuccessStatusCode)
                return true;

            var execption = await result.GetException();
            ConsoleColor.Red.WriteLine($"Error while Deleting record {id:N}: {execption.Error.Detail}");
            return false;
        }

        #endregion
    }
}
