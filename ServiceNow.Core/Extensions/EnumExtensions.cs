using System;
using System.ComponentModel;

namespace SNow.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get data from Description Attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns>The description text value</returns>
        public static string ToDescription<T>(this T type) where T : Enum
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])type
               .GetType()
               .GetField(type.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
