using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Features.Products.Commands;
using ProductManagement.Application.Features.Products.Queries;
using ProductManagement.Domain.Entities;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var allProducts = await _mediator.Send(new GetAllProductsQuery());

            var totalCount = allProducts.Count;
            var pagedProducts = allProducts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Items = pagedProducts
            });
        }
 

        // PUT: api/products
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}
