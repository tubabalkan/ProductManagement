using Xunit;
using Moq;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Application.Features.Products.Commands;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Tests.Features.Products.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _handler = new CreateProductCommandHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Should_Create_Product_And_Return_Id()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Price = 99.99m,
                Stock = 50
            };

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            _mockRepo.Verify(x => x.AddAsync(It.Is<Product>(
                p => p.Name == command.Name &&
                     p.Price == command.Price &&
                     p.Stock == command.Stock)), Times.Once);

            Assert.NotEqual(Guid.Empty, result);
        }
    }
}
