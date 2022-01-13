using SNow.Core;
using SNow.Core.Models;
using System.Text.Json.Serialization;

namespace Snow.Test
{
    [SnowTable("snow_table_name")]
    public class DumpServer : ServiceNowBaseModel
    {
        [JsonPropertyName("strange_name")]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
