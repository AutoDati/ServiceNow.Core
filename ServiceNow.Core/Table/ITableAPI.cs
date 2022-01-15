using SNow.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SNow.Core
{
    public interface ITableAPI
    {
        string RequestUrl { get; }
        ITableAPI SetHeaders(List<KeyValuePair<string, string>> entries);
        /// <summary>
        /// The query must have only those operators
        /// and, or, like, =, !=, startsWith, endsWith
        /// see <see href="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html">SN Rest Operators</see>
        /// </summary>
        /// <param name="expression">String that has access to the table model
        /// ex.: x => $"Name like Something and Age = 10"</param>
        ITableAPI WithQuery(string query);
        //ITableAPI<TModel> Where(Expression<Func<TModel, bool>> expression);
        /// <summary>
        /// The maximum number of results returned per page (default: 10,000)
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        ITableAPI Limit(int limit);
        ITableAPI OrderBy(string orderBy);
        ITableAPI OrderByDesc(string orderByDesc);
        /// <summary>
        /// List of properties to return,
        /// impacts the size of the response
        /// </summary>
        /// <param name="expressions"></param>
        ITableAPI Select(string[] fields);
        /// <summary>
        /// Makes the actual HTTP Request
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns>An generic json element of type Array</returns>
        Task<List<JsonElement>> ToListAsync(int? pageNumber = null);
        Task<JsonElement> GetByIdAsync(Guid id);
        Task<bool> Delete(Guid id);
        Task<JsonElement> Create(object model);
        Task<JsonElement> Update(Guid id, object data, bool excludeLinks = true);

    }

    public interface ITableApi<TModel> where TModel : ServiceNowBaseModel
    {
        string RequestUrl { get; }

        /// <summary>
        /// Set query parameters to the API request using Where clause.<br/>
        /// Don't use it togueter with "WithQuery"
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        ITableApi<TModel> Where(Expression<Func<TModel, bool>> expr);

        ITableApi<TModel> SetHeaders(List<KeyValuePair<string, string>> entries);
        /// <summary>
        /// The query must have only those operators
        /// and, or, like, =, !=, startsWith, endsWith
        /// see <see href="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html">SN Rest Operators</see>
        /// </summary>
        /// <param name="expression">String that has access to the table model
        /// ex.: x => $"{x.Name} like Something and {x.Age} = 10"</param>
        ITableApi<TModel> WithQuery(Expression<Func<TModel, string>> expression);
        //ITableAPI<TModel> Where(Expression<Func<TModel, bool>> expression);
        /// <summary>
        /// The maximum number of results returned per page (default: 10,000)
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        ITableApi<TModel> Limit(int limit);
        ITableApi<TModel> OrderBy(Expression<Func<TModel, object>> expression);
        ITableApi<TModel> OrderByDesc(Expression<Func<TModel, object>> expression);
        /// <summary>
        /// List of properties to return,
        /// impacts the size of the response
        /// </summary>
        /// <param name="expressions">you can pass more than one parameter ex.: x=> x.Name, x=> x.Age</param>
        ITableApi<TModel> Select(params Expression<Func<TModel, object>>[] expressions);
        /// <summary>
        /// Makes the actual Http Request
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<List<TModel>> ToListAsync(int? pageNumber = null);
        Task<TModel> GetByIdAsync(Guid id);
        Task<bool> Delete(Guid id);
        Task<TModel> Create(object model);
        Task<TModel> Update(Guid? id, object data, bool excludeLinks);
    }
}
