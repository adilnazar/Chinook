using System.Text.Json.Serialization;

namespace Chinook.ClientModels.Request
{
    public class CommonRequestModel
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
