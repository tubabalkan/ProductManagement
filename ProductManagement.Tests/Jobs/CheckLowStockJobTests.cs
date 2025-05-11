using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Tests.Jobs
{
    public class CheckLowStockJobTests
    {
        [Fact]
        public async Task Should_Log_Products_Whose_Stock_Is_Low()
        {
            // Arrange
            var mockProductRepo = new Mock<IProductRepository>();
            var mockLogRepo = new Mock<ILowStockLogRepository>();

            var lowStockProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = "LowStockItem",
                Stock = 5,
                Price = 100
            };

            mockProductRepo.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Product> { lowStockProduct });

            var job = new CheckLowStockJob(mockProductRepo.Object, mockLogRepo.Object);

            // Act
            await job.ExecuteAsync();

            // Assert
            mockLogRepo.Verify(x => x.AddAsync(It.Is<LowStockLog>(log =>
                log.ProductId == lowStockProduct.Id &&
                log.Stock == lowStockProduct.Stock &&
                log.ProductName == lowStockProduct.Name
            )), Times.Once);
        }
    }
}
