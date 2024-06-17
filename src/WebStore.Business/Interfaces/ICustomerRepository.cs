using WebStore.Business.Models;

namespace WebStore.Business.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> PasswordSignInAsync(string email, string password);
        //Task<List<Supplier>> GetCustomersAsync();

        //Task<Supplier> GetSupplierAddressTask(Guid id);
        //Task<Supplier> GetSupplierProductsAddress(Guid id);
    }
}
