using BlogEngine.DataSource.Models;
using BlogEngine.DataSource.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers;

[Route("api/entry")]
[ApiController]
public class EntryController : ControllerBase
{
    private readonly IEntryProvider _entryProvider;

    public EntryController(IEntryProvider entryProvider)
    {
        _entryProvider = entryProvider;
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult> GetEntry(string slug)
    {
        var result = await _entryProvider.GetEntryAsync(slug);

        return Ok(result);
    }

    [HttpPost("navigate")]
    public async Task<ActionResult> Navigate([FromBody] NavigationCriteria navigationCriteria)
    {
        var result = await _entryProvider.GetEntriesAsync(navigationCriteria);

        return Ok(result);
    }
}
