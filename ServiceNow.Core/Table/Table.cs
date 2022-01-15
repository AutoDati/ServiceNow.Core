
using Microsoft.Extensions.Logging;
using SNow.Core.Extensions;
using SNow.Core.Models;
using SNow.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ServiceNow.Core.Test")]
namespace SNow.Core
{
    public class Table : TableBase, ITableAPI
    {

        public Table(IServiceNow serviceNow, string tableName, ILogger logger) : base(serviceNow, tableName, logger)
        {
        }

        public async Task<JsonElement> GetByIdAsync(Guid id)
        {

            var url = $"{SN.BaseAddress}/table/{_tableName}/{id:N}";
            return await _httpClient.GetActionResultAsync<JsonElement>(url, SN.AuthenticateAsync, _logger);

        }

        public ITableAPI Limit(int limit)
        {
            _pageSize = limit;
            return this;
        }

        public ITableAPI OrderBy(string orderBy)
        {
            _order += string.IsNullOrEmpty(_order) ? "ORDERBY" + orderBy : "^ORDERBY" + orderBy;
            return this;
        }

        public ITableAPI OrderByDesc(string orderByDesc)
        {
            _order += string.IsNullOrEmpty(_order) ? "ORDERBYDESC" + orderByDesc : "^ORDERBYDESC" + orderByDesc;
            return this;
        }

        public ITableAPI Select(string[] fields)
        {
            _select = fields.ToList();
            return this;
        }

        public ITableAPI SetHeaders(List<KeyValuePair<string, string>> entries)
        {
            foreach (var entry in entries)
            {
                _httpClient.DefaultRequestHeaders.Add(entry.Key, entry.Value);
            }
            return this;
        }

        public async Task<JsonElement> Update(Guid id, object data, bool excludeReferenceLinks = true)
        {
            var excludeLinks = excludeReferenceLinks ? "?sysparm_exclude_reference_link=true" : "";
            var url = $"{SN.BaseAddress}/table/{_tableName}/{id:N}{excludeLinks}";
            var result = await _httpClient.PutActionResultAsync<JsonElement>(url, data, SN.AuthenticateAsync);

            return result;
        }

        public ITableAPI WithQuery(string query)
        {
            _query = Query.Parse(query);
            _currentPage = 0;
            return this;
        }

        public async Task<List<JsonElement>> ToListAsync(int? page = null)
        {
            var url = page == null ? RequestGetUrl : RequestUrl + $"&sysparm_offset={page * _pageSize}";
            _currentPage++;
            var result = await _httpClient.GetActionResultAsync<List<JsonElement>>(url, SN.AuthenticateAsync, _logger);

            if (result.Count == 0)
                _currentPage = 0;

            return result;
        }

        public async Task<JsonElement> Create(object model)
        {
            var url = $"{SN.BaseAddress}/table/{_tableName}";

            var result = await _httpClient.PostActionResultAsync<JsonElement>(url, model, SN.AuthenticateAsync);
            return result;
        }
    }
    public class Table<TModel> : TableBase, ITableApi<TModel> where TModel : ServiceNowBaseModel
    {
        private List<(string PropName, string AttName)> _props = new List<(string PropName, string AttName)>();
        public string _where;

        public Table(IServiceNow serviceNow, string tableName, ILogger logger = null) : base(serviceNow, tableName, logger)
        {
            _select = ClassReflections.GetPropertieNamesInJsonFormat<TModel>();
            _tableName = tableName;
            _props = ClassReflections.GetJsonPropertyNameData<TModel>();
        }

        public Table(IServiceNow serviceNow, ILogger logger = null) : base(serviceNow, "", logger)
        {
            _select = ClassReflections.GetPropertieNamesInJsonFormat<TModel>();
            _props = ClassReflections.GetJsonPropertyNameData<TModel>();
            var attrs = typeof(TModel).GetCustomAttributes(typeof(SnowTableAttribute), true);

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
        }
        public ITableApi<TModel> Limit(int limit)
        {
            _pageSize = limit;
            return this;
        }

        public ITableApi<TModel> OrderBy(Expression<Func<TModel, object>> expression)
        {
            var propName = (expression.Body as MemberExpression ??
                     ((UnaryExpression)expression.Body).Operand as MemberExpression).Member.Name;
            var attName = _props.Find(p => p.PropName == propName).AttName;
            _order += string.IsNullOrEmpty(_order) ? "ORDERBY" + (attName ?? propName.ToLower()) : "^ORDERBY" + (attName ?? propName.ToLower());
            return this;
        }

        public ITableApi<TModel> OrderByDesc(Expression<Func<TModel, object>> expression)
        {
            var propName = (expression.Body as MemberExpression ??
                   ((UnaryExpression)expression.Body).Operand as MemberExpression).Member.Name;
            var attName = _props.Find(p => p.PropName == propName).AttName;
            _order += string.IsNullOrEmpty(_order) ? "ORDERBYDESC" + (attName ?? propName.ToLower()) : "^ORDERBYDESC" + (attName ?? propName.ToLower());
            return this;
        }

        public ITableApi<TModel> Select(params Expression<Func<TModel, object>>[] expressions)
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

        public ITableApi<TModel> Where(Expression<Func<TModel, bool>> expr)
        {
            var visitor = new PrintingVisitor<TModel>(expr);
            visitor.Visit(expr);
            _query = visitor.query;
            _query = _query.Replace("(", "").Replace(")", "");
            _query = Query.Parse(_query);
            _currentPage = 0;
            return this;
        }

        public ITableApi<TModel> SetHeaders(List<KeyValuePair<string, string>> entries)
        {
            foreach (var entry in entries)
            {
                _httpClient.DefaultRequestHeaders.Add(entry.Key, entry.Value);
            }
            return this;
        }

        public ITableApi<TModel> WithQuery(Expression<Func<TModel, string>> expression)
        {
            //TODO: Should we be using expression tree to extract the data?
            var arguments = (expression.Body as MethodCallExpression)?.Arguments;
            var query = arguments?[0].ToString().Replace("\"", "") ?? (expression.Body as ConstantExpression).Value.ToString();
            var queryArguments = new List<string>();
            if (arguments != null)
                foreach (var arg in arguments.Skip(1))
                {
                    if (arg.GetType().Name == "NewArrayInitExpression")
                    {
                        throw new Exception("More than 3 Arguments in lambda expression!!!");
                        //var argArray = arg.ToString().Replace("new [] {", "").Replace("}", "").Split(',');
                        //foreach (Expression subarg in (arg as MethodCallExpression).Arguments)
                        //{
                        //    var propName = subarg.ToString().Split('.')[1].Replace(", Object)", "");
                        //    var attName = _props.Find(p => p.PropName == propName).AttName;

                        //    //var externalArgName = arg.ToString().Contains(')') ? arg.ToString().Split(')')[1].Replace(".","") : null;
                        //    var externalArgValue = subarg.ToString().Contains("value") ? Expression.Lambda<Func<Object>>(subarg).Compile()().ToString() : null;
                        //    queryArguments.Add(externalArgValue ?? attName ?? propName.ToLower());
                        //}
                    }
                    else
                    {
                        var propName = arg.ToString().Split('.')[1].Replace(", Object)", "").Replace(")", "");
                        var attName = _props.Find(p => p.PropName == propName).AttName;

                        //var externalArgName = arg.ToString().Contains(')') ? arg.ToString().Split(')')[1].Replace(".","") : null;
                        var externalArgValue = arg.ToString().Contains("value") ? Expression.Lambda<Func<object>>(arg).Compile()().ToString() : null;
                        queryArguments.Add(externalArgValue ?? attName ?? propName.ToLower());
                    }
                }

            var result = string.Format(query, queryArguments.ToArray());
            _query = Query.Parse(result);
            _currentPage = 0;
            return this;
        }

        public async Task<List<TModel>> ToListAsync(int? page)
        {
            if (SN.Token == null && SN.BasicAuthParams == null)
                await SN.AuthenticateAsync();
            if (_httpClient.DefaultRequestHeaders.Authorization == null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SN.Token);
            var url = page == null ? RequestGetUrl : RequestUrl + $"&sysparm_offset={page * _pageSize}";
            var result = await _httpClient.GetActionResultAsync<List<TModel>>(url, SN.AuthenticateAsync, _logger);
            _currentPage++;

            if (result?.Count == 0)
                _currentPage = 0;

            return result;
        }

        async Task<TModel> ITableApi<TModel>.GetByIdAsync(Guid id)
        {
            var url = $"{SN.BaseAddress}/table/{_tableName}/{id:N}";
            return await _httpClient.GetActionResultAsync<TModel>(url, SN.AuthenticateAsync, _logger);
        }

        async Task<bool> ITableApi<TModel>.Delete(Guid id)
        {
            return await Delete(id);
        }

        async Task<TModel> ITableApi<TModel>.Create(object model)
        {
            var url = $"{SN.BaseAddress}/table/{_tableName}";

            var result = await _httpClient.PostActionResultAsync<TModel>(url, model, SN.AuthenticateAsync);
            return result;
        }

        async Task<TModel> ITableApi<TModel>.Update(Guid? id, object data, bool excludeReferenceLinks = true)
        {
            var excludeLinks = excludeReferenceLinks ? "?sysparm_exclude_reference_link=true" : "";
            var url = $"{SN.BaseAddress}/table/{_tableName}/{id:N}{excludeLinks}";

            var result = await _httpClient.PutActionResultAsync<TModel>(url, data, SN.AuthenticateAsync);

            return result;
        }
    }

    /// <summary>
    /// Base class to share props and methods used in typed and non typed table classes
    /// </summary>
    public class TableBase
    {
        public IServiceNow SN { get; set; }
        protected HttpClient _httpClient;
        protected readonly ILogger _logger;


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
        protected string _query;
        protected int? _pageSize = 10000;
        protected string _tableName;
        protected string _order = "";
        protected List<string> _select;
        protected int _currentPage = 0;

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
                allArgs.Add(query);

            if (withCurrentPage && _pageSize != null)
                allArgs.Add($"sysparm_offset={_currentPage * _pageSize}");

            allArgs.Add("sysparm_exclude_reference_link=true");

            return $"{mainUri}{string.Join("&", allArgs)}";
        }

        public async Task<bool> Delete(Guid id)
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
