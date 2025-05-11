using MediatR;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
