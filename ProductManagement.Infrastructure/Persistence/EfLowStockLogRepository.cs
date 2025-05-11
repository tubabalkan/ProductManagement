using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Infrastructure.Persistence
{
    public class EfLowStockLogRepository : ILowStockLogRepository
    {
        private readonly ProductDbContext _context;

        public EfLowStockLogRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LowStockLog log)
        {
            await _context.LowStockLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
