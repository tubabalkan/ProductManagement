using MediatR;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };

            await _productRepository.AddAsync(product);
            return product.Id;
        }
    }
}
