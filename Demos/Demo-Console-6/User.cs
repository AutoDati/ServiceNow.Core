using SNow.Core;
using SNow.Core.Models;
using System;
using System.Text.Json.Serialization;

namespace Models6
{
    [SnowTable("sys_user")]
    public class User : ServiceNowBaseModel
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("u_city_code")]
        public string CityCode { get; set; }
        public string Email { get; set; }

        [JsonPropertyName("user_name")]
        public string LanID { get; set; }

        [JsonPropertyName("sys_created_on")]
        public DateTime CreatedOn { get; set; }

        public override string ToString()
        {
            return $"user:{Name}, State:{State}, CityCode:{CityCode}, email:{Email} => {Id} in {CreatedOn}";
        }
    }
}

//sys_updated_onONToday@javascript:gs.beginningOfToday()@javascript:gs.endOfToday()
//sys_created_on < javascript:gs.dateGenerate('2024-02-14', '00:00:00')