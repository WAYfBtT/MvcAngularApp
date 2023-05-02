using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularMVC.Controllers
{
    [Route("api/url")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService) => _urlService = urlService;

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UrlModel>>> GetAll()
            => Ok(await _urlService.GetAllAsync());

        [HttpGet("own")]
        public async Task<ActionResult<IEnumerable<UrlModel>>> GetAllOwn()
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            if (!int.TryParse(claim?.Value, out int userId))
                return Unauthorized();
            return Ok(await _urlService.GetByUserIdAsync(userId));
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<UrlModel>> Get(int id)
            => Ok(await _urlService.GetByIdAsync(id));

        [HttpPost()]
        public async Task<ActionResult> Create(UrlModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    ModelState
                    .Select(x => x.Value)
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList());

            var id = await _urlService.AddAsync(model);
            return CreatedAtAction(nameof(Get), id);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            await _urlService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpDelete("{id}/own")]
        //[Authorize()]
        public async Task<ActionResult> Delete(UrlModel model)
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            if (claim == null)
                return Forbid();
            await _urlService.DeleteAsync(model);
            return Ok();
        }
    }
}
