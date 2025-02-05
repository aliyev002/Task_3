using Task_3.Entities;

namespace Task_3.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllByKeyAsync(string key = "");
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task DeleteAsync(int id);
        Task UpdateAsync(Product product);
    }
}
