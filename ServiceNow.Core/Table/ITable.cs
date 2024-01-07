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
        /// <summary>
        /// 
        /// </summary>
        string RequestUrl { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        ITable SetHeaders(List<KeyValuePair<string, string>> entries);
        /// <summary>
        /// The query must have only those operators <br/>
        /// and, or, like, =, !=, startsWith, endsWith <br/>
        /// see <see href="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html">SN Rest Operators</see>
        /// </summary>
        /// <param name="query">String that has access to the table model <br/>
        /// ex.: x => $"Name like Something and Age = 10"</param>
        ITable WithQuery(string query);
        /// <summary>
        /// The maximum number of results returned per page (default: 10,000)
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        ITable Limit(int limit);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        ITable OrderBy(string orderBy);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderByDesc"></param>
        /// <returns></returns>
        ITable OrderByDesc(string orderByDesc);
        /// <summary>
        /// List of properties to return, <br/>
        /// impacts the size of the response
        /// </summary>
        /// <param name="fields"></param>
        ITable Select(string[] fields);
        /// <summary>
        /// Makes the actual HTTP request
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns>An generic json element of type Array</returns>
        Task<List<JsonElement>> ToListAsync(int? pageNumber = null);

        /// <summary>
        /// Makes HTTP requests to get all data (from all pages)
        /// </summary>
        /// <returns></returns>
        Task<List<JsonElement>> AllToListAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonElement> GetByIdAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<JsonElement> CreateAsync(object model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <param name="excludeLinks"></param>
        /// <returns></returns>
        Task<JsonElement> UpdateAsync(Guid id, object data, bool excludeLinks = true);

    }

    /// <summary>
    /// Handle ServiceNow tables API
    /// </summary>
    public interface ITable<T> where T : ServiceNowBaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        string RequestUrl { get; }

        /// <summary>
        /// Set query parameters to the API request using Where clause.<br/>
        /// Order matters so x => x.id == id works while x => id == x.id don't. <br/> 
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        ITable<T> Where(Expression<Func<T, bool>> expr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        ITable<T> SetHeaders(List<KeyValuePair<string, string>> entries);
        
        /// <summary>
        /// The maximum number of results returned per page (default: 10,000)
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        ITable<T> Limit(int limit);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ITable<T> OrderBy(Expression<Func<T, object>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Makes HTTP requests to get all data (from all pages)
        /// </summary>
        /// <returns></returns>
        Task<List<T>> AllToListAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<T> CreateAsync(object model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <param name="excludeLinks"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(Guid? id, object data, bool excludeLinks = true);
    }
}
