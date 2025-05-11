using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using ProductManagement.Application.Features.Products.Commands;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Tests.Features.Products.Commands
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _handler = new DeleteProductCommandHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Should_Call_DeleteAsync_With_Correct_Id()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var command = new DeleteProductCommand(productId);

            // Act
            await _handler.Handle(command, default);

            // Assert
            _mockRepo.Verify(x => x.DeleteAsync(productId), Times.Once);
        }
    }
}
