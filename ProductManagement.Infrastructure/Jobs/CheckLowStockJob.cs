using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;

public class CheckLowStockJob
{
    private readonly IProductRepository _productRepository;
    private readonly ILowStockLogRepository _logRepository;

    public CheckLowStockJob(
        IProductRepository productRepository,
        ILowStockLogRepository logRepository)
    {
        _productRepository = productRepository;
        _logRepository = logRepository;
    }

    public async Task ExecuteAsync()
    {
        var lowStockProducts = (await _productRepository.GetAllAsync())
            .Where(p => p.Stock < 10)
            .ToList();

        foreach (var product in lowStockProducts)
        {
            var log = new LowStockLog
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Stock = product.Stock,
                LoggedAt = DateTime.UtcNow
            };

            await _logRepository.AddAsync(log);
        }
    }
}
