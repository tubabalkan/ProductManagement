using MediatR;

namespace ProductManagement.Application.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
