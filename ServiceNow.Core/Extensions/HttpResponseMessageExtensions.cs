using SNow.Core.Utils;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SNow.Core.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Deserialize an ServiceNow API response
        /// </summary>
        /// <typeparam name="TValue">type to be deserialized to</typeparam>
        /// <param name="response">Deserialized List or a single element of type <typeparamref name="TValue"/></param>
        /// <returns></returns>
        public static async Task<TValue> DeserializeAsync<TValue>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                ConsoleColor.Red.WriteLine($"Failed to call the web API: {response.StatusCode}");
                Console.WriteLine();
                string content = await response.Content.ReadAsStringAsync();

                // Note that if you got reponse.Code == 403 and reponse.content.code == "Authorization_RequestDenied"
                // this is because the tenant admin as not granted consent for the application to call the Web API                
                ConsoleColor.Red.WriteLine($"Content: {content}");
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden && content.Contains("Authorization_RequestDenied"))
                    ConsoleColor.Yellow.WriteLine("Probably the tenant admin has not granted consent for the application to call the Web API");

                throw new Exception(content);
            }

            var plainResult = await response.Content.ReadAsStringAsync();
            //Response from ServiceNow contain result when more them one item is returned
            if (!plainResult.IsJsonValid())
                Console.WriteLine("this should never log because the above Method trows exception if string is not a json");

            var hasResult = plainResult.Length >=10 && plainResult.Substring(0, 10).Contains("result");

            ServiceNowResult plainResultList = null;

            try
            {
                if (hasResult)
                    plainResultList = JsonSerializer.Deserialize<ServiceNowResult>(plainResult, JsonConverterOptions.CustomSerializationOptions);

                var result = JsonSerializer.Deserialize<TValue>(hasResult ? plainResultList.result.ToString() : plainResult, JsonConverterOptions.CustomSerializationOptions);
                return result;
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteLine($"Failed to deserialize<{typeof(TValue)}> => {ex} with value {plainResult}");

                throw ex;
            }
        }

        public static async Task<ExceptionResponse> GetException(this HttpResponseMessage httpResponseMessage)
        {
            string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            ExceptionResponse exceptionResponse = JsonSerializer.Deserialize<ExceptionResponse>(responseContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return exceptionResponse;
        }

        #region Helper classes
        private class ServiceNowResult
        {
            public object result { get; set; }
        }
        #endregion
    }
}
