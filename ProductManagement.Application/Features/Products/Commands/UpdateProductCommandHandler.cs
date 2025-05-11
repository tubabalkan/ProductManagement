using MediatR;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Application.Features.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByIdAsync(request.Id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found.");
            }

            existingProduct.Name = request.Name;
            existingProduct.Price = request.Price;
            existingProduct.Stock = request.Stock;

            await _productRepository.UpdateAsync(existingProduct);
            return Unit.Value;
        }
    }
}
