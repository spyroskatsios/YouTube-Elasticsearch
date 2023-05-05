using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Songs.Api.Persistence;

namespace Songs.Api.Controllers;

[ApiController]
[Route("genres")]
public class GenresController : ControllerBase
{
    private readonly IAppDbContext _context;

    public GenresController(IAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var genres = await _context.Genres.AsNoTracking().ToListAsync(cancellationToken);
        return Ok(genres);
    }
}