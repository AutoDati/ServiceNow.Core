using SNow.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SNow.Core.SetImport
{
    public class ImportSet : ImportSetBase, IImportSet
    {
        public ImportSet(IServiceNow serviceNow, string tableName) : base(serviceNow, tableName)
        {
        }

        public async Task<ImportSetResponse> Import(object data) {
            var url = $"{SN.BaseAddress}/import/{_tableName}";

            if(data is IEnumerable)
                url += "/insertMultiple";

            var payload = new
            {
                records = data
            };

            var result = await _httpClient.PostActionResultAsync<ImportSetResponse>(url, payload, SN.AuthenticateAsync);
            return result;
        }
    }

    public class ImportSetBase
    {
        public IServiceNow SN { get; set; }
        protected HttpClient _httpClient;

        protected string _tableName;

        public ImportSetBase(IServiceNow serviceNow, string tableName)
        {
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

        }

        protected bool IsEnumerable(Type type)
        {
            if (type == typeof(String))
                return false;

            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }
    }
}
