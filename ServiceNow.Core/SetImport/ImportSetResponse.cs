using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SNow.Core.SetImport
{
    public class ImportSetResponse
    {
        [JsonPropertyName("import_set_id")]
        public string ImportSetId { get; set; }

        [JsonPropertyName("import_set")]
        public string ImportSet { get; set; }

        [JsonPropertyName("multi_import_set_id")]
        public string MultiImportSetId { get; set; }

        public bool IsMultiImport => String.IsNullOrEmpty(MultiImportSetId) is false;
    }
}
