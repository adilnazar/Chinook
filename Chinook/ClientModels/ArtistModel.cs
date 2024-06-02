using System.Text.Json.Serialization;

namespace Chinook.ClientModels
{
    public class ArtistModel
    {
        [JsonPropertyName("artistId")]
        public long ArtistId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("albumCount")]
        public int AlbumCount { get; set; }
    }
}
