using Nest;

namespace Songs.Api.Elastic;

public class ElasticSong
{
    [Number(NumberType.Long)]
    public long Id { get; set; }
    
    [Text]
    public string Title { get; set; }
    
    [Text]
    public string AlbumTitle { get; set; }
    
    [Keyword]
    public string AlbumReleaseDate { get; set; }
    
    [Text]
    public string ArtistName { get; set; }
    
    [Keyword]
    public string Genre { get; set; }
}