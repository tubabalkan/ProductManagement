using MediatR;
using ProductManagement.Domain.Entities;
using System.Collections.Generic;

namespace ProductManagement.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
    }
}
