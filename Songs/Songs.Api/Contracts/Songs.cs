namespace Songs.Api.Contracts;

public record SearchSongsRequest
{
    public string? SearchText { get; init; }
    public string? Genre { get; init; }

    private readonly int _pageNumber;
    public int PageNumber
    {
        get => _pageNumber; 
        init => _pageNumber = value > 0 ? value : 1;
    }

    private readonly int _pageSize;
    public int PageSize  
    {
        get => _pageSize; 
        init => _pageSize = value > 20 ? value : 20;
    }
}

public record SongResponse(long Id, string Title, string AlbumTitle, string AlbumReleaseDate, string ArtistName, string Genre, DateOnly ReleaseDate);

public record SearchSongsResponse(IEnumerable<SongResponse> Songs, int PageNumber, int PageSize,
    long TotalCount, int TotalPages);
    
public record SongResponseWithScore(long Id, string Title, string AlbumTitle, string AlbumReleaseDate,
    string ArtistName, string Genre, DateOnly ReleaseDate, double Score);

public record SearchSongsIncludeScoresResponse(IEnumerable<SongResponseWithScore> Songs, int PageNumber, int PageSize,
    int TotalCount, int TotalPages);

