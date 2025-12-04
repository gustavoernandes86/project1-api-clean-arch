using Microsoft.AspNetCore.Mvc;
using Project1.Application.DTOs;
using Project1.Application.Services;
using System;
using System.Threading.Tasks;

namespace Project1.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;
        public ProductsController(ProductService service){
             _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item is null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest req)
        {
            var dto = await _service.CreateAsync(req.Name, req.Price, req.Description);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest req)
        {
            var ok = await _service.UpdateAsync(id, req.Name, req.Price, req.Description);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }

        public record CreateProductRequest(string Name, decimal Price, string? Description);
        public record UpdateProductRequest(string Name, decimal Price, string? Description);
    }
}
