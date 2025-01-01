using System;
using DynamicPixels.GameService.Models.outputs;
using Newtonsoft.Json;

namespace DynamicPixels.GameService.Services.Authentication.Models
{
    [Serializable]
    public class LoginResponse : BaseResponse
    {
        [JsonProperty("user")]
        public User.Models.User User { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("connection")]
        public ConnectionInfo Connection { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}