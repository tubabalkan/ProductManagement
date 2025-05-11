using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Application.Features.Products.Queries;

namespace ProductManagement.Tests.Features.Products.Queries
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly GetProductByIdQueryHandler _handler;

        public GetProductByIdQueryHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _handler = new GetProductByIdQueryHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Should_Return_Product_When_It_Exists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Name = "Test",
                Price = 50,
                Stock = 5
            };

            _mockRepo.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);

            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal("Test", result.Name);
        }

        [Fact]
        public async Task Should_Return_Null_When_Product_Not_Exists()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _mockRepo.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync((Product?)null);

            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.Null(result);
        }
    }
}
