using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace SNow.Core.Utils
{
    public static class ClassReflections
    {
        public static List<(string PropName, string AttName)> GetJsonPropertyNameData<T>()
        {
            var _dict = new List<(string PropName, string AttName)>();
            var props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    JsonPropertyNameAttribute authAttr = attr as JsonPropertyNameAttribute;
                    if (authAttr != null)
                    {
                        string propName = prop.Name;
                        string auth = authAttr.Name;

                        _dict.Add((propName, auth));
                    }
                }
            }
            return _dict;
        }

        /// <summary>
        /// Return a list of json property names found, 
        /// if no json name is found per property, it return the property name instead
        /// </summary>
        /// <typeparam name="T">Model type to extract json property names</typeparam>
        /// <returns></returns>
        public static List<string> GetPropertieNamesInJsonFormat<T>()
        {
            var _dict = new List<string>();
            var props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                string propName = prop.Name;
                string jsonName = null;

                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    JsonPropertyNameAttribute jsonAttr = attr as JsonPropertyNameAttribute;
                    if (jsonAttr != null)
                    {
                        jsonName = jsonAttr.Name;

                    }
                }
                _dict.Add(jsonName ?? ConvertCamelToSnake(propName));
            }
            return _dict;
        }

        public static string ConvertCamelToSnake(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Regular expression to find uppercase letters and add an underscore before them
            string result = Regex.Replace(input, "([a-z0-9])([A-Z])", "$1_$2");

            // Convert to lower case
            return result.ToLower();
        }
    }


}
