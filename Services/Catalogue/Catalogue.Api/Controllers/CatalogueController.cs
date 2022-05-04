using Catalogue.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalogue.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogueController : ControllerBase
    {
        private ICatalogueServices _services;
        public CatalogueController(ICatalogueServices services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Catalogue>>> GetCatalouge()
        {
            return Ok(await _services.GetCatalogueAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Catalogue>> GetCatalouge(string? id)
        {
            if (id == null)
                return BadRequest();
            return Ok(await _services.GetCatalogueByIdAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult<Models.Catalogue>> AddToCatalouge([FromBody] Models.Catalogue catalogue)
        {
            if (catalogue == null)
                return BadRequest();
            var route = new Uri($"{Request.Protocol.Split("/")[0].ToLower()}://{Request.Host}/api/v1/Catalogue/{catalogue.Id}");
            var result = await _services.AddItemToCatalogue(catalogue);
            return Created(route, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Models.Catalogue>> UpdateItemInCatalouge(string? id,[FromBody] Models.Catalogue catalogue)
        {
            if (catalogue == null || id==null)
                return BadRequest();
            return Ok(await _services.UpdateItemInCatalogue(id,catalogue));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveFromCatalouge(string? id)
        {
            if (id == null)
                return BadRequest();
            await _services.RemoveItemFromCatalogue(id);
            return Ok(new {Message=$"Item with Id {id} Removed Sucessfully"});
        }
    }
}
