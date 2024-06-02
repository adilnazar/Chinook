using System.Text.Json.Serialization;

namespace Chinook.ClientModels;

public class Playlist
{
    [JsonPropertyName("playlistId")]
    public long PlaylistId { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("tracks")]
    public List<PlaylistTrack> Tracks { get; set; }
}