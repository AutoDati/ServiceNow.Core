using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNow.Core.Authentication
{
    public class BasicAuthenticationConfig
    {
        /// <summary>
        /// Web API base URL
        /// </summary>
        public string BaseAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Reads the configuration from a json file
        /// </summary>
        /// <param name="path">Path to the configuration json file</param>
        /// <returns>AuthenticationConfig read from the json file</returns>
        public static BasicAuthenticationConfig ReadFromJsonFile(string path)
        {
            IConfigurationRoot Configuration;

            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path);

            Configuration = builder.Build();
            return Configuration.Get<BasicAuthenticationConfig>();
        }
    }
}
