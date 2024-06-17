using WebStore.Business.Models;

namespace WebStore.Business.Interfaces
{
    public interface ICustomerService
    {
        Task CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task RemoveAsync(Guid id);
    }
}
