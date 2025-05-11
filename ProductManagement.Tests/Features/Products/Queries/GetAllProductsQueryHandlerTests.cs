using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Application.Features.Products.Queries;

namespace ProductManagement.Tests.Features.Products.Queries
{
    public class GetAllProductsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly GetAllProductsQueryHandler _handler;

        public GetAllProductsQueryHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _handler = new GetAllProductsQueryHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Should_Return_All_Products()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "P1", Price = 100, Stock = 10 },
                new Product { Id = Guid.NewGuid(), Name = "P2", Price = 200, Stock = 5 }
            };

            _mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _handler.Handle(new GetAllProductsQuery(), default);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name == "P1");
            Assert.Contains(result, p => p.Name == "P2");
        }
    }
}
