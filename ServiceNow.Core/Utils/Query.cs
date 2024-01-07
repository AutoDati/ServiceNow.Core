using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SNow.Core.Utils
{
    /// <summary>
    /// <see cref="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html"/>
    /// </summary>
    public static class Query
    {
        /// <summary>
        /// Parse query to ServiceNow format
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string Parse(string query)
        {
            var response = Regex.Replace(query, @"\s+", " ");
                  
            response = response
                .Replace(" = ", "=")
                .Replace(" ^ ", "^")
                .Replace(" != ", "!=")
                .Replace(" AND ", "^")
                .Replace(" and ", "^")
                .Replace(" OR ", "^OR")
                .Replace(" or ", "^OR")
                .Replace(" like ", "LIKE")
                .Replace(" LIKE ", "LIKE")
                .Replace(" Like ", "LIKE")
                .Replace(" StartsWith ", "STARTSWITH")
                .Replace(" startsWith ", "STARTSWITH")
                .Replace(" startswith ", "STARTSWITH")
                .Replace(" EndsWith ", "ENDSWITH")
                .Replace(" endsWith ", "ENDSWITH")
                .Replace(" endswith ", "ENDSWITH");

            return response;
        }
    }
}
