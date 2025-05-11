using MediatR;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Application.Features.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
