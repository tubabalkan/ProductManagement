using MediatR;

namespace ProductManagement.Application.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<Guid> // Dönen değer: yeni ürün Id
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
