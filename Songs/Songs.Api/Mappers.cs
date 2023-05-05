using Nest;
using Songs.Api.Contracts;
using Songs.Api.Elastic;
using Songs.Api.Entities;

namespace Songs.Api;

public static class Mappers
{
    public static ElasticSong ToElasticSong(this Song song)
        => new()
        {
            Id = song.Id,
            Title = song.Title,
            AlbumTitle = song.Album!.Title,
            AlbumReleaseDate = song.Album.ReleaseDate.ToString("yyyy-MM-dd"),
            ArtistName = song.Album.Artist!.Name,
            Genre = song.Album.Genre!.Name
        };
    
    public static SearchParameters ToSearchParameters(this SearchSongsRequest request)
        => new(request.SearchText, request.Genre, request.PageSize * (request.PageNumber - 1) , request.PageSize);
    
    public static SongResponse ToSongResponse(this ElasticSong song)
        => new(song.Id, song.Title, song.AlbumTitle, song.AlbumReleaseDate, song.ArtistName, song.Genre, DateOnly.Parse(song.AlbumReleaseDate));
    
    public static SongResponseWithScore ToSongResponseWithScore(this IHit<ElasticSong> song)
        => new(song.Source.Id, song.Source.Title, song.Source.AlbumTitle, song.Source.AlbumReleaseDate, 
            song.Source.ArtistName, song.Source.Genre, DateOnly.Parse(song.Source.AlbumReleaseDate), song.Score ?? 0);
}