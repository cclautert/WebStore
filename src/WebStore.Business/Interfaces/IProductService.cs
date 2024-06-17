using WebStore.Business.Models;

namespace WebStore.Business.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Guid id);
    }
}
