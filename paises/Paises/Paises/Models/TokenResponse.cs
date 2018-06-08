

namespace Paises.Models
{
    using Newtonsoft.Json;
    using System;

    public class TokenResponse
    {
        #region Propiedades

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expire_in")]
        public int ExpireIn { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = ".issuued")]
        public DateTime Issuued { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }

        public bool IsRemembered { get; set; }

        public string Password { get; set; }
        #endregion
      
    }
}
