using System.Text.Json.Serialization;

namespace Chinook.ClientModels;

public class PlaylistTrack
{
    [JsonPropertyName("trackId")]
    public long TrackId { get; set; }

    [JsonPropertyName("trackName")]
    public string? TrackName { get; set; }

    [JsonPropertyName("albumTitle")]
    public string? AlbumTitle { get; set; }

    [JsonPropertyName("artistName")]
    public string? ArtistName { get; set; }

    [JsonPropertyName("isFavorite")]
    public bool IsFavorite { get; set; }

}