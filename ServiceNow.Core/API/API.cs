using Microsoft.Extensions.Logging;
using Polly;
using SNow.Core.Extensions;
using SNow.Core.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SNow.Core.API
{
    public class API : APIBase, IAPI
    {
        public API(IServiceNow serviceNow, string nameSpace, ILogger logger) : base(serviceNow, nameSpace, logger)
        {

        }
        public async Task<bool>  DeleteAsync(string endpoint, Guid id)
        {
            return await deleteAsync(endpoint, id);
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            if (SN.Token == null && SN.BasicAuthParams == null)
                await SN.AuthenticateAsync();
            if (_httpClient.DefaultRequestHeaders.Authorization == null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SN.Token);
            var requestUri = $"{SN.BaseAddress.Replace("/now","")}/{_nameSpace}/{endpoint}";
            return await Policy.Handle<Exception>()
                .RetryAsync(2, (exception, retry) =>
                {
                    _logger?.LogWarning("Error in HttpGet from {link}: {e}", requestUri, exception.InnerException);
                    _logger?.LogWarning("Retrying turn: {e}", retry);
                })
                .ExecuteAsync(async () =>
                {
                    var response = await _httpClient.GetAsync(requestUri);
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var token = await SN.AuthenticateAsync();
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        response = await _httpClient.GetAsync(requestUri);
                    }

                    await HandleUnssucessfullRequestAsync(response);

                    //In case you try to get an specif ID but is was not found.
                    if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NotFound)
                        return default;

                    return response;
                });
        }

        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent data)
        {
            if (SN.Token == null && SN.BasicAuthParams == null)
                await SN.AuthenticateAsync();
            if (_httpClient.DefaultRequestHeaders.Authorization == null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SN.Token);
            var requestUri = $"{SN.BaseAddress.Replace("/now","")}/{_nameSpace}/{endpoint}";
            return await Policy.Handle<Exception>()
                .RetryAsync(2, (exception, retry) =>
                {
                    _logger?.LogWarning("Error in HttpPost from {link}: {e}", requestUri, exception.InnerException);
                    _logger?.LogWarning("Retrying turn: {e}", retry);
                })
                .ExecuteAsync(async () =>
                {
                    var response = await _httpClient.PostAsync(requestUri, data);
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var token = await SN.AuthenticateAsync();
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        response = await _httpClient.PostAsync(requestUri, data);
                    }

                    await HandleUnssucessfullRequestAsync(response);

                    //In case you try to get an specif ID but is was not found.
                    if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NotFound)
                        return default;

                    return response;
                });
        }

        public async Task<HttpResponseMessage> PutAsync(string endpoint, Guid id, HttpContent data)
        {
            if (SN.Token == null && SN.BasicAuthParams == null)
                await SN.AuthenticateAsync();
            if (_httpClient.DefaultRequestHeaders.Authorization == null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SN.Token);
            var requestUri = $"{SN.BaseAddress.Replace("/now","")}/{_nameSpace}/{endpoint}";
            return await Policy.Handle<Exception>()
                .RetryAsync(2, (exception, retry) =>
                {
                    _logger?.LogWarning("Error in HttpPut from {link}: {e}", requestUri, exception.InnerException);
                    _logger?.LogWarning("Retrying turn: {e}", retry);
                })
                .ExecuteAsync(async () =>
                {
                    var response = await _httpClient.PutAsync(requestUri, data);
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var token = await SN.AuthenticateAsync();
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        response = await _httpClient.PutAsync(requestUri, data);
                    }

                    await HandleUnssucessfullRequestAsync(response);

                    //In case you try to get an specif ID but is was not found.
                    if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NotFound)
                        return default;

                    return response;
                });
        }

    }

    public class API<T> : APIBase, IAPI<T> where T : ServiceNowBaseModel
    {
        public API(IServiceNow serviceNow, string nameSpace, ILogger logger) : base(serviceNow, nameSpace, logger)
        {
            
        }
        public async Task<bool> DeleteAsync(string endpoint, Guid id)
        {
            return await DeleteAsync(endpoint, id);
        }

        public async Task<List<T>> GetAsync(string endpoint)
        {
            var url = $"{SN.BaseAddress.Replace("/now","")}/{_nameSpace}/{endpoint}";
            return await _httpClient.GetActionResultAsync<List<T>>(url, SN.AuthenticateAsync, _logger);
        }

        public async Task<T> GetAsync(string endpoint, bool singleResponse)
        {
            var url = $"{SN.BaseAddress.Replace("/now","")}/{_nameSpace}/{endpoint}";
            return await _httpClient.GetActionResultAsync<T>(url, SN.AuthenticateAsync, _logger);
        }


        public async Task<T> PostAsync(string endpoint, object data)
        {
            var url = $"{SN.BaseAddress.Replace("/now","")}/{_nameSpace}/{endpoint}";
            var result = await _httpClient.PostActionResultAsync<T>(url, data, SN.AuthenticateAsync);
            return result;
        }

        public async Task<T> PutAsync(string endpoint, Guid id, object data)
        {
            var url = $"{SN.BaseAddress.Replace("/now","")}/{_nameSpace}/{endpoint}";
            var result = await _httpClient.PutActionResultAsync<T>(url, data, SN.AuthenticateAsync);

            return result;
        }

    }

    public class APIBase
    {
        public IServiceNow SN { get; set; }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected HttpClient _httpClient;
        protected readonly ILogger _logger;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


        protected string _nameSpace;
        public APIBase(IServiceNow serviceNow, string nameSpace, ILogger logger)
        {
            if (logger != null)
                _logger = logger;

            SN = serviceNow;
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _nameSpace = nameSpace;
            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _httpClient.DefaultRequestHeaders.Connection.ParseAdd("keep-alive");
            if (SN.BasicAuthParams != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", SN.BasicAuthParams);


        }

        /// <inheritdoc/>
        public async Task<bool> deleteAsync(string endpoint, Guid id)
        {
            if (SN.Token == null)
                await SN.AuthenticateAsync();
            var url = $"{SN.BaseAddress.Replace("/now","")}/{_nameSpace}/{endpoint}/{id:N}";

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


          protected static async Task HandleUnssucessfullRequestAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new UnauthorizedAccessException($"Could not Authenticate in ServiceNow! Status: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {content}");
                    case HttpStatusCode.NotFound:
                        break;
                    default:
                        throw new HttpRequestException($"Error while requesting to ServiceNow Status: {response.StatusCode}, Reason: {response.ReasonPhrase}, Content: {content}");
                }
            }
        }
    }
}
