using System.Text.Json.Serialization;

namespace Chinook.ClientModels.Request
{
    public class AddTrackToPlaylistModel
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("trackId")]
        public long TrackId { get; set; }

        [JsonPropertyName("playlistId")]
        public long? PlaylistId { get; set; }

        [JsonPropertyName("newPlaylistName")]
        public string? NewPlaylistName { get; set; }
    }
}
