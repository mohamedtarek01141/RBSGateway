using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RBSGateway.DTO.Resource;
using RBSGateway.Services.ResourceServices;

namespace RBSGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourcesController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var resources = await _resourceService.GetAllAsync();
            return resources.Any()? Ok(resources) : NotFound(resources);
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resource = await _resourceService.GetByIdAsync(id,1,1);
            if (resource == null)
                return NotFound("Resource not found");

            return Ok(resource);
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(CreateResourceDto createDto)
        {
            var resource = await _resourceService.CreateAsync(createDto);
            if (resource == null)
                return BadRequest("Failed to create resource");

            return Ok(resource);
        }
        [HttpPut("update")]
        public async Task<ActionResult> Update( UpdateResourceDto updateDto)
        {
            var result = await _resourceService.UpdateAsync(updateDto);
            return result ? Ok(result) : BadRequest("Failed to update resource");
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {


            var result = await _resourceService.DeleteAsync(Id,1,1);
            return result ? Ok(result) : BadRequest("Failed to delete resource");
        }
    }
}
