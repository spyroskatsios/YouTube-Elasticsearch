namespace Songs.Api.Entities;

public class Album
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public DateOnly ReleaseDate { get; set; }
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public long ArtistId { get; set; }
    public Artist? Artist { get; set; }
    public IEnumerable<Song> Songs { get; set; } = new List<Song>();
}