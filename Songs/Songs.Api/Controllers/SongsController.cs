using Microsoft.AspNetCore.Mvc;
using Songs.Api.Contracts;
using Songs.Api.Elastic;

namespace Songs.Api.Controllers;

[ApiController]
[Route("songs")]
public class SongsController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SongsController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery]SearchSongsRequest request, CancellationToken cancellationToken)
    {
        var parameters = request.ToSearchParameters();
        var songsResponse = await _searchService.SearchAsync(parameters, cancellationToken);
        var songs = songsResponse.Documents;
        var count = songsResponse.Total;
        var response = new SearchSongsResponse(songs.Select(x => x.ToSongResponse()), request.PageNumber,
            request.PageSize, count, (int)Math.Ceiling(count / (double)request.PageSize));
        return Ok(response);
    }
    
    [HttpGet("include-score")]
    public async Task<IActionResult> SearchIncludeScore([FromQuery]SearchSongsRequest request, CancellationToken cancellationToken)
    {
        var parameters = request.ToSearchParameters();
        var songsResponse = await _searchService.SearchAsync(parameters, cancellationToken);
        var count = (int)songsResponse.Total;
        var response = new SearchSongsIncludeScoresResponse(songsResponse.Hits.Select(x => x.ToSongResponseWithScore()), request.PageNumber,
            request.PageSize, count, (int)Math.Ceiling(count  / (double)request.PageSize));
        return Ok(response);
    }

}