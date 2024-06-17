using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Business.Models.Validations;

namespace WebStore.Business.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository,
                                 INotifier notificador) : base(notificador)
        {
            _customerRepository = customerRepository;
        }

        public async Task CreateAsync(Customer customer)
        {
            if (!RunValidation(new CustomerValidation(), customer)) return;

            await _customerRepository.CreateAsync(customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
            if (!RunValidation(new CustomerValidation(), customer)) return;

            await _customerRepository.UpdateAsync(customer);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _customerRepository.RemoveAsync(id);
        }
    }
}
