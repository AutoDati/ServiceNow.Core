using System;
using System.Text.Json;

namespace SNow.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsJsonValid(this string txt)
        {
            try {
                return JsonDocument.Parse(txt) != null; 
            } catch {
                throw new Exception($"Response is not Json: {txt}");
            }
        }
    }
}
