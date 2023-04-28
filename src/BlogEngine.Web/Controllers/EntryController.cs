using BlogEngine.DataSource.Index;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers;

[Route("api/entry")]
[ApiController]
public class EntryController : ControllerBase
{
    public EntryController(IIndexManager indexManager)
    {

    }
    [HttpGet("{id}")]
    public ActionResult GetEntry(string id)
    {
        return Ok();
    }
}
