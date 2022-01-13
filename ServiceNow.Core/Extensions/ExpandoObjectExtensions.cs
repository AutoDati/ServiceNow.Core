using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace SNow.Core.Extensions
{
    public static class ExpandoObjectExtensions
    {
        public static void UpdateProp(this ExpandoObject expando, string prop, object value)
        {
            var map = (IDictionary<string, Object>)expando;
            if (map.ContainsKey(prop))
                map[prop] = value;
            foreach (var currentValue in map.Values)
            {
                if (currentValue is ExpandoObject)
                    UpdateProp((ExpandoObject)currentValue, prop, value);
            }
        }
    }
}
