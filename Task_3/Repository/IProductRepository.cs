using Task_3.Entities;

namespace Task_3.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(string key);
        Task AddAsync(Product product);
        Task<Product> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(Product product);

    }
}
