using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

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
                string propName = prop.Name.ToLower();
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
                _dict.Add(jsonName ?? propName);
            }
            return _dict;
        }
    }
}
