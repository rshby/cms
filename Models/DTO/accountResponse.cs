using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace cms.Models.DTO
{
   public class accountResponse
   {
      [JsonPropertyName("id")]
      public int? Id { get; set; }

      [JsonProperty("email")]
      public string? Email { get; set; }

      [JsonProperty("username")]
      public string? Username { get; set; }

      [JsonProperty("password")]
      public string? Password { get; set; }

      [JsonProperty("otp", NullValueHandling = NullValueHandling.Ignore)]
      [System.Text.Json.Serialization.JsonIgnore(Condition =  JsonIgnoreCondition.WhenWritingNull)]
      public string? Otp { get; set; }

      [JsonProperty("expired_otp", NullValueHandling = NullValueHandling.Ignore)]
      public string? ExpiredOtp { get; set; }

      [JsonProperty("created_at")]
      public string? CreatedAt { get; set; }

      [JsonPropertyName("user_id")]
      public int? UserId { get; set; }
   }
}
