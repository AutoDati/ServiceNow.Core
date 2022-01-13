using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.Json;

namespace SNow.Core.Extensions
{
    public static class JsonElementExtensions
    {
        /// <summary>
        /// Display the property name and value for each property on an object or 
        /// display for all elements inside an array.
        /// </summary>
        /// <param name="element"></param>
        public static void Display(this JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Array)
            {
                for (int i = 0; i < element.GetArrayLength(); i++)
                {
                    var props = element[i].EnumerateObject();
                    foreach (var prop in props)
                    {
                        ConsoleColor.Cyan.Write("\t" + prop.Name);
                        Console.Write(" = ");
                        ConsoleColor.DarkYellow.WriteLine(prop.Value.ToString());
                    }
                    Console.WriteLine("");
                }
            }
            else if (element.ValueKind == JsonValueKind.Object)
            {
                foreach (var prop in element.EnumerateObject())
                {
                    ConsoleColor.Cyan.Write("\t" + prop.Name);
                    Console.Write(" = ");
                    ConsoleColor.DarkYellow.WriteLine(prop.Value.ToString());
                }
                Console.WriteLine("");
            }
        }

        //public static ExpandoObject ToObject(this JsonElement element)
        //{
        //    if (element.ValueKind != JsonValueKind.Object)
        //        return null;

        //    var response = new ExpandoObject();
        //    foreach (var prop in element.EnumerateObject())
        //        response.TryAdd(prop.Name, prop.Value);

        //    return response;
        //}
    }
}
