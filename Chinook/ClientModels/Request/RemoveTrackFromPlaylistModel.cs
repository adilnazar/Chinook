using System.Text.Json.Serialization;

namespace Chinook.ClientModels.Request
{
    public class RemoveTrackFromPlaylistModel
    {
        [JsonPropertyName("trackId")]
        public long TrackId { get; set; }

        [JsonPropertyName("playlistId")]
        public long PlaylistId { get; set; }
    }
}
