using SNow.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SNow.Core.API
{
    /// <summary>
    /// Handle ServiceNow API
    /// </summary>
    public interface IAPI
    {
        Task<HttpResponseMessage> GetAsync(string endpoint);
        Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent data);
        Task<HttpResponseMessage> PutAsync(string endpoint, Guid id, HttpContent data);
        Task<bool> DeleteAsync(string endpoint, Guid id);
    }

    /// <summary>
    /// Handle ServiceNow API
    /// </summary>
    public interface IAPI<T> where T : ServiceNowBaseModel
    {
        Task<List<T>> GetAsync(string endpoint);
        Task<T> GetAsync(string endpoint, bool singleResponse);
        Task<T> PostAsync(string endpoint, object data);
        Task<T> PutAsync(string endpoint, Guid id, object data);
        /// <summary>
        /// Must have and attribute id in the path.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string endpoint, Guid id);
    }
}
