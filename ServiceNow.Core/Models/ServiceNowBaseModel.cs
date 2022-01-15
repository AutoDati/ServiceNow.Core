using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SNow.Core.Models
{
    /// <summary>
    /// Base class where ServiceNow models can derive from
    /// </summary>
    public class ServiceNowBaseModel
    {
        /// <summary>
        /// Ignore when writing
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sys_id")]
        public Guid? Id { get; set; }
    }
}
