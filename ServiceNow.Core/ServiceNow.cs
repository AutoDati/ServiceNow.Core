using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using SNow.Core.Authentication;
using SNow.Core.SetImport;
using SNow.Core.Models;
using SNow.Core.ServiceCatalog;
using SNow.Core.Utils;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SNow.Core
{
    public class ServiceNow : IServiceNow
    {
        protected AuthenticationConfig _authConfiguration;

#if NETCOREAPP
        protected ITokenAcquisition _tokenAcquisition;
#endif
        public string Token { get; set; }

        protected string _baseAddress;

        string IServiceNow.BaseAddress => _baseAddress;

        /// <summary>
        /// You configuration must have a session named AzureAd
        /// </summary>
        /// <param name="configuration"></param>
        public ServiceNow( IConfiguration configuration, JsonConverter[] customConverters = null)
        {
            _authConfiguration = AuthenticationConfig.ReadFromConfiguration(configuration.GetSection("AzureAd"));


            if (String.IsNullOrEmpty(_authConfiguration.ClientSecret) && String.IsNullOrEmpty(_authConfiguration.CertificateName))
                throw new ArgumentNullException($"A ClientSecret or an CertificateName must be passed to authenticate! client secret = {_authConfiguration.ClientSecret}, certification name = {_authConfiguration.CertificateName}");


            _baseAddress = _authConfiguration.BaseAddress;
            if (customConverters != null)
            {
                JsonConverterOptions.ConfigureCustomSerializers(customConverters);
            }
        }

        public ServiceNow(AuthenticationConfig configuration, JsonConverter[] customConverters = null)
        {

            if (String.IsNullOrEmpty(configuration.ClientSecret) && String.IsNullOrEmpty(configuration.CertificateName))
                throw new ArgumentNullException($"A ClientSecret or an CertificateName must be passed to authenticate! client secret = {configuration.ClientSecret}, certification name = {configuration.CertificateName}");

            _authConfiguration = configuration;
            _baseAddress = configuration.BaseAddress;
            if(customConverters != null)
            {
                JsonConverterOptions.ConfigureCustomSerializers(customConverters);
            }
        }

        public ITableAPI<TModel> UsingTable<TModel>(string tableName, ILogger logger = null) where TModel : ServiceNowBaseModel
        {
            return new Table<TModel>(this,tableName, logger) {};
        }

        public ITableAPI<TModel> UsingTable<TModel>(ILogger logger = null) where TModel : ServiceNowBaseModel
        {
            return new Table<TModel>(this, logger) { };
        }

        public ITableAPI UsingTable(string tableName, ILogger logger = null)
        {
            return new Table(this,tableName, logger) {};
        }

        /// <summary>
        /// Updates the ServiceNow Token internal property
        /// </summary>
        /// <returns> Token from ServiceNow</returns>
        async Task<string> IServiceNow.AuthenticateAsync()
        {
#if NETCOREAPP
            var auth = await Authenticator.AuthenticateAsync(_authConfiguration, _tokenAcquisition);
#else
            var auth = await Authenticator.AuthenticateAsync(_authConfiguration);
#endif

            Token = auth?.AccessToken;
            return Token;
        }

        public ICatalogService UsingCatalog(Guid catalogItemId)
        {
            return new CatalogService(this,catalogItemId) {};
        }

        public ICatalogService<T> UsingCatalog<T>(Guid catalogItemId) where T : ServiceNowBaseModel
        {
            return new CatalogService<T>(this,catalogItemId) {};
        }


        public IImportSet UsingImportSet(string tableName)
        {
            return new ImportSet(this, tableName);
        }
    }
}
