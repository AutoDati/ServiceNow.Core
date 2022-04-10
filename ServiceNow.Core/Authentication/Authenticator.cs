using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using SNow.Core.Extensions;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SNow.Core.Authentication
{
    public static class Authenticator
    {
        private static IConfidentialClientApplication _app;

#if NETCOREAPP
        /// <summary>
        /// Authenticate in to AAD based the configuration provided
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="_tokenAcquisition"></param>
        /// <returns></returns>
        public static async Task<AuthenticationResult> AuthenticateAsync(AuthenticationConfig configuration, ITokenAcquisition _tokenAcquisition = null)
        {
            bool isClientCredentialFlow = AppUsesCredentialFlow(configuration);

            if (isClientCredentialFlow)
            {
                return await AuthenticateWithCredentialFlow(configuration);
            }
            //authorization code flow
            else
            {
                var identityData = await _tokenAcquisition.GetAuthenticationResultForUserAsync(new[] { configuration.Scope });
                return identityData;              
            }
        }

#else
        /// <summary>
        /// Authenticate in to AAD based the configuration provided
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static async Task<AuthenticationResult> AuthenticateAsync(AuthenticationConfig configuration)
        {
            bool isClientCredentialFlow = AppUsesCredentialFlow(configuration);

            if (isClientCredentialFlow)
            {
                return await AuthenticateWithCredentialFlow(configuration);
            }
            //authorization code flow
            else
            {
                throw new NotImplementedException("Authorization Code Flow not implemented for Framework 4.x");
            }
        }
#endif

        private static bool AppUsesCredentialFlow(AuthenticationConfig config)
        {
            return config.Scope.EndsWith(".default");
        }

        private static bool AppUsesClientSecret(AuthenticationConfig config)
        {
            if (!String.IsNullOrWhiteSpace(config.ClientSecret))
            {
                return true;
            }

            else if (!String.IsNullOrWhiteSpace(config.CertificateName))
            {
                return false;
            }

            Console.WriteLine("You are not using client secret or certificate. Those are preferable and are set in appsettings.json file.");
            return false;
        }

        private static X509Certificate2 ReadCertificate(string certificateName)
        {
            if (string.IsNullOrWhiteSpace(certificateName))
            {
                throw new ArgumentException("certificateName should not be empty. Please set the CertificateName setting in the appsettings.json", certificateName);
            }
            CertificateDescription certificateDescription = CertificateDescription.FromStoreWithDistinguishedName(certificateName);
            DefaultCertificateLoader defaultCertificateLoader = new DefaultCertificateLoader();
            defaultCertificateLoader.LoadIfNeeded(certificateDescription);
            return certificateDescription.Certificate;
        }

        private static async Task<AuthenticationResult> AuthenticateWithCredentialFlow(AuthenticationConfig configuration)
        {
            bool isUsingClientSecret = AppUsesClientSecret(configuration);
            if (isUsingClientSecret)
            {
                _app = ConfidentialClientApplicationBuilder.Create(configuration.ClientId)
                    .WithClientSecret(configuration.ClientSecret)
                    .WithAuthority(new Uri(configuration.Authority))
                    .Build();
            }
            else
            {
                //throw new NotImplementedException("/using Certificate is not implemented yet, try to use a client secret instead...");
                X509Certificate2 certificate = ReadCertificate(configuration.CertificateName);
                _app = ConfidentialClientApplicationBuilder.Create(configuration.ClientId)
                    .WithCertificate(certificate)
                    .WithAuthority(new Uri(configuration.Authority))
                    .Build();
            }

            // With client credentials flows the scopes is ALWAYS of the shape "resource/.default", as the 
            // application permissions need to be set statically (in the portal or by PowerShell), and then granted by
            // a tenant administrator
            string[] scopes = new string[] { configuration.Scope };

            AuthenticationResult identityData;
            try
            {
                identityData = await _app.AcquireTokenForClient(scopes).ExecuteAsync();
                return identityData;
            }
            catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            {
                // Invalid scope. The scope has to be of the form "https://resourceurl/.default"
                // Mitigation: change the scope to be as expected
                ConsoleColor.Red.WriteLine("Scope provided is not supported, expected format \"https://resourceurl/.default\"");
            }

            return null;
        }
    }
}
