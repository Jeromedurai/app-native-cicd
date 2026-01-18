using System;
using Newtonsoft.Json;

namespace CICD.Models
{
	public class ApiResult
	{
        [JsonProperty("data")]
        public dynamic Data { get; set; }
        [JsonProperty("data")]
        public dynamic Exception { get; set; }
    }
    public class AuthenticationResponseData
    {
        [JsonProperty("data")]
        public AuthenticationResponse Data { get; set; }

        [JsonProperty("data")]
        public dynamic Exception { get; set; }
    }

    public class AuthenticationResponse
    {
        [JsonProperty("isAuthenticated")]
        public bool IsAuthenticated { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("roleName")]
        public string RoleName { get; set; }
    }
}

