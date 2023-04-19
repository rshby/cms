using Newtonsoft.Json;

namespace cms.Models.DTO
{
   [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
   public class APIMessage<T> where T : class
   {
      [JsonProperty("status_code")]
      public int? StatusCode { get; set; }

      [JsonProperty("status")]
      public string? Status { get; set; }

      [JsonProperty("message")]
      public string? Message { get; set; }

      [JsonProperty("data")]
      public T? Data { get; set; }
   }
}
