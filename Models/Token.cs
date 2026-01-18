using System;
using Newtonsoft.Json;

namespace CICD.Models
{
    public class Token
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("userRole")]
        public string UserRole { get; set; }
        [JsonProperty("tenantId")]
        public string TenantId { get; set; }
        [JsonProperty("locationId")]
        public string LocationId { get; set; }
        [JsonProperty("internal")]
        public bool Internal { get; set; }
    }
}

