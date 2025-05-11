using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using ProductManagement.Application.Features.Products.Commands;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Tests.Features.Products.Commands
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _handler = new UpdateProductCommandHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Should_Update_Existing_Product()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = new Product
            {
                Id = productId,
                Name = "Old",
                Price = 100,
                Stock = 10
            };

            _mockRepo.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(existingProduct);

            var command = new UpdateProductCommand
            {
                Id = productId,
                Name = "Updated",
                Price = 150,
                Stock = 20
            };

            // Act
            await _handler.Handle(command, default);

            // Assert
            _mockRepo.Verify(x => x.UpdateAsync(It.Is<Product>(p =>
                p.Id == productId &&
                p.Name == "Updated" &&
                p.Price == 150 &&
                p.Stock == 20
            )), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_When_Product_Not_Found()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockRepo.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync((Product?)null);

            var command = new UpdateProductCommand
            {
                Id = productId,
                Name = "Doesn't matter",
                Price = 0,
                Stock = 0
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, default));
        }
    }
}
