using SNow.Core.Models;
using System;
using System.Text.Json.Serialization;

namespace Models
{
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

        public override string ToString()
        {
            return $"user:{Name}, State:{State}, CityCode:{CityCode}, email:{Email}";
        }
    }
}
