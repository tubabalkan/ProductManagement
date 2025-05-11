using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<List<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
