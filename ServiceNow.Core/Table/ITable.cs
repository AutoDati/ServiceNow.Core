using SNow.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SNow.Core
{
    /// <summary>
    /// Handle ServiceNow tables API
    /// </summary>
    public interface ITable
    {
        string RequestUrl { get; }
        ITable SetHeaders(List<KeyValuePair<string, string>> entries);
        /// <summary>
        /// The query must have only those operators
        /// and, or, like, =, !=, startsWith, endsWith
        /// see <see href="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html">SN Rest Operators</see>
        /// </summary>
        /// <param name="expression">String that has access to the table model
        /// ex.: x => $"Name like Something and Age = 10"</param>
        ITable WithQuery(string query);
        /// <summary>
        /// The maximum number of results returned per page (default: 10,000)
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        ITable Limit(int limit);
        ITable OrderBy(string orderBy);
        ITable OrderByDesc(string orderByDesc);
        /// <summary>
        /// List of properties to return,
        /// impacts the size of the response
        /// </summary>
        /// <param name="expressions"></param>
        ITable Select(string[] fields);
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

    public interface ITable<T> where T : ServiceNowBaseModel
    {
        string RequestUrl { get; }

        /// <summary>
        /// Set query parameters to the API request using Where clause.<br/>
        /// Don't use it together with "WithQuery"
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        ITable<T> Where(Expression<Func<T, bool>> expr);

        ITable<T> SetHeaders(List<KeyValuePair<string, string>> entries);
        /// <summary>
        /// The query must have only those operators
        /// and, or, like, =, !=, startsWith, endsWith
        /// see <see href="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html">SN Rest Operators</see>
        /// </summary>
        /// <param name="expression">String that has access to the table model
        /// ex.: x => $"{x.Name} like Something and {x.Age} = 10"</param>
        ITable<T> WithQuery(Expression<Func<T, string>> expression);
        
        /// <summary>
        /// The maximum number of results returned per page (default: 10,000)
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        ITable<T> Limit(int limit);
        ITable<T> OrderBy(Expression<Func<T, object>> expression);
        ITable<T> OrderByDesc(Expression<Func<T, object>> expression);
        /// <summary>
        /// List of properties to return,
        /// impacts the size of the response
        /// </summary>
        /// <param name="expressions">you can pass more than one parameter ex.: x=> x.Name, x=> x.Age</param>
        ITable<T> Select(params Expression<Func<T, object>>[] expressions);
        /// <summary>
        /// Makes the actual HTTP Request
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<List<T>> ToListAsync(int? pageNumber = null);
        Task<T> GetByIdAsync(Guid id);
        Task<bool> Delete(Guid id);
        Task<T> Create(object model);
        Task<T> Update(Guid? id, object data, bool excludeLinks);
    }
}
