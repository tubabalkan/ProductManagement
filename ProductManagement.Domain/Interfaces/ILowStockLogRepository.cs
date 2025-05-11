using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Interfaces
{
    public interface ILowStockLogRepository
    {
        Task AddAsync(LowStockLog log);
    }
}
