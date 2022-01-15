using Microsoft.Extensions.Logging;
using Polly;
using SNow.Core.Authentication;
using SNow.Core.Utils;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SNow.Core.Extensions
{
    /// <summary>
    /// Handle Deserialize on HTTP requests
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Use to handle GET communications between API's
        /// </summary>
        /// <typeparam name="T">A class containing or not props with attributes of type [JsonPropertyName]</typeparam>
        /// <param name="requestUri">Full request URI</param>
        /// <param name="authenticate">Execute the authentication method</param>
        /// <returns>An instance of the provided class, can also be a List<TValue></returns>
        public static async Task<T> GetActionResultAsync<T>(this HttpClient client, string requestUri, Func<Task<string>> authenticate = null, ILogger logger = null)
        {
            return await Policy.Handle<Exception>()
                .RetryAsync(2, (exception, retry) =>
                {
                    logger?.LogError("Error in HttpGet from {link}: {e}", requestUri, exception.InnerException);
                    logger?.LogError("Retrying turn: {e}", retry);
                })
                .ExecuteAsync<T>(async () =>
                {
                    var response = await client.GetAsync(requestUri);
                    if (response.StatusCode == HttpStatusCode.Unauthorized && authenticate != null)
                    {
                        var token = await authenticate();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        response = await client.GetAsync(requestUri);
                    }

                    await HandleUnssucessfullRequestAsync(response);

                    //In case you try to get an specif ID but is was not found.
                    if (!response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return default;

                    return await response.DeserializeAsync<T>();
                });
        }

        /// <summary>
        /// Use to handle POST communication between API's
        /// </summary>
        /// <typeparam name="T">A class containing props with attributes of type [JsonPropertyName]</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">Full request uri</param>
        /// <param name="data">Data to be sent, usually a model</param>
        /// <param name="authenticate"></param>
        /// <returns>An instance of the provided class, can also be a List TValue </returns>
        public static async Task<T> PostActionResultAsync<T>(this HttpClient client, string requestUri, object data, Func<Task<string>> authenticate)
        {
            var json = JsonSerializer.Serialize(data, JsonConverterOptions.CustomSerializationOptions);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            {
                var response = await client.PostAsync(requestUri, httpContent);
                // once we try to Post httpContent is disposed
                if (response.StatusCode == HttpStatusCode.Unauthorized && authenticate != null)
                {
                    var token = await authenticate();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(requestUri, httpContent);
                    httpContent.Dispose();
                }

                if (typeof(T) == typeof(HttpStatusCode))
                {
                    var status = response.StatusCode;
                    return (T)Convert.ChangeType(status, typeof(T));
                }

                await HandleUnssucessfullRequestAsync(response);
                return await response.DeserializeAsync<T>();
            }
        }

        /// <summary>
        /// Use to handle PUT communication between API's
        /// </summary>
        /// <typeparam name="T">A class containing props with attributes of type [JsonPropertyName]</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">Full request uri</param>
        /// <param name="data">Data to be sent, usually a model</param>
        /// <param name="authenticate"></param>
        /// <returns>An instance of the provided class, can also be a List TValue </returns>
        public static async Task<T> PutActionResultAsync<T>(this HttpClient client, string requestUri, object data, Func<Task<string>> authenticate)
        {
            var json = JsonSerializer.Serialize(data, JsonConverterOptions.CustomSerializationOptions);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            {
                var response = await client.PutAsync(requestUri, httpContent);
                // once we try to Post httpContent is disposed
                if (response.StatusCode == HttpStatusCode.Unauthorized && authenticate != null)
                {
                    var token = await authenticate();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PutAsync(requestUri, httpContent);
                    httpContent.Dispose();
                }

                if (typeof(T) == typeof(HttpStatusCode))
                {
                    var status = response.StatusCode;
                    return (T)Convert.ChangeType(status, typeof(T));
                }

                await HandleUnssucessfullRequestAsync(response);
                return await response.DeserializeAsync<T>();

            }
        }

        private static async Task HandleUnssucessfullRequestAsync(HttpResponseMessage response)
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
