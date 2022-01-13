using SNow.Core.Extensions;
using SNow.Core.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SNow.Core.ServiceCatalog
{
    public class CatalogService : CatalogServiceBase, ICatalogService
    {
 
        public CatalogService(IServiceNow serviceNow, Guid catalogItem) : base(serviceNow, catalogItem)
        {

        }

        public async Task<JsonElement> Request(object data)
        {
            var url = $"{_baseAddress}/sn_sc/servicecatalog/items/{_catalogueItemId}/order_now";

            var result = await _httpClient.PostActionResultAsync<JsonElement>(url, data, SN.AuthenticateAsync);
            return result;
        }
    }

    public class CatalogService<TModel> : CatalogServiceBase, ICatalogService<TModel> where TModel : ServiceNowBaseModel
    {
        public CatalogService(IServiceNow serviceNow, Guid catalogItem) : base (serviceNow, catalogItem)
        {

        }

        async Task<TModel> ICatalogService<TModel>.Request(object variables)
        {
            var url = $"{_baseAddress}/sn_sc/servicecatalog/items/{_catalogueItemId}/order_now";

            var payload = new {
                variables,
                sysparm_quantity = "1"
            };

            var result = await _httpClient.PostActionResultAsync<TModel>(url, payload, SN.AuthenticateAsync);
            return result;
        }
    }

    public class CatalogServiceBase
    {
        public IServiceNow SN { get; set; }
        protected HttpClient _httpClient;

        protected new string _baseAddress => SN.BaseAddress.Replace("/now", "");
        protected string _catalogueItemId;

        public CatalogServiceBase(IServiceNow serviceNow, Guid catalogItemId)
        {
            SN = serviceNow;
            _catalogueItemId = catalogItemId.ToString("N");
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _httpClient = new HttpClient(handler);

            //var defaultRequestHeaders = _httpClient.DefaultRequestHeaders;
            //if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
            //{
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                _httpClient.DefaultRequestHeaders.Connection.ParseAdd("keep-alive");
            //}
        }
    }
}
