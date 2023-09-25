

using System.Text.Json.Serialization;

namespace cms.Models.DTO
{
   public class APIMessage<T> where T : class
   {
      [JsonPropertyName("status_code")]
      public int? StatusCode { get; set; }

      [JsonPropertyName("status")]
      public string? Status { get; set; }

      [JsonPropertyName("message")]
      public string? Message { get; set; }

      [JsonPropertyName("data")]
      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      public T? Data { get; set; }
   }
}
