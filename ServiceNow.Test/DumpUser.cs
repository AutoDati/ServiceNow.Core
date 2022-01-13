using SNow.Core.Models;
using System.Text.Json.Serialization;

namespace Snow.Test
{
    public class DumpUser : ServiceNowBaseModel
    {
        [JsonPropertyName("strange_name")]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
