using MediatR;

namespace ProductManagement.Application.Features.Products.Commands
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
