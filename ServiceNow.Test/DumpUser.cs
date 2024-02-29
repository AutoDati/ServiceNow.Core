using SNow.Core;
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

    [SnowFilter("nameLikeBottero")]
    public class DumpUser2 : ServiceNowBaseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }

    }

    [SnowTable("cmdb_rel_ci")]
    [SnowFilter("installed_on.sys_class_name!=cmdb_ci_pc_hardware^display_nameNOT LIKEManagement Studio^display_nameSTARTSWITHSQL Server^display_nameENDSWITHIntegration Services^ORdisplay_nameLIKEEngine Services^ORdisplay_nameLIKEAnalysis Services^ORdisplay_nameENDSWITHReporting Services^ORdisplay_name=Microsoft Power BI Report Server")]
    public class Relations: ServiceNowBaseModel
    {

    }
}
