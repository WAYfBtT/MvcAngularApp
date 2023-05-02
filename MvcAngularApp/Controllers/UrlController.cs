using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [Authorize]
        public async Task<ActionResult<UrlModel>> Get(int id)
        {
            var res = await _urlService.GetByIdAsync(id);
            if (res == null)
                return NotFound();
            return Ok(res);
        }

        [HttpPost()]
        public async Task<ActionResult> Create(UrlModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        ));
            try
            {
                var id = await _urlService.AddAsync(model);
                return CreatedAtAction(nameof(Get), id);
            }
            catch (UrlShortenerApplicationException ex)
            {
                AddErrors(ex);
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            try
            {
                await _urlService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (UrlShortenerApplicationException ex)
            {
                AddErrors(ex);
                return BadRequest(ModelState);
            }

            
        }

        [HttpDelete("{id}/own")]
        [Authorize()]
        public async Task<ActionResult> Delete(UrlModel model)
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            if (claim == null)
                return Forbid();

            try
            {
                await _urlService.DeleteAsync(model);
                return Ok();
            }
            catch (UrlShortenerApplicationException ex)
            {
                AddErrors(ex);
                return BadRequest(ModelState);
            }
        }

        private void AddErrors(UrlShortenerApplicationException ex)
        {
            foreach (var error in ex.Errors)
                foreach (var message in error.Value)
                    ModelState.AddModelError(error.Key, message);
        }
    }
}
