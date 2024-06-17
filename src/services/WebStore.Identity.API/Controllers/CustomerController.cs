using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Identity.API.ViewModels;

namespace WebStore.Identity.API.Controllers
{
    [Route("api/customer")]
    public class CustomerController : MainController
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerService _customerService;

        public CustomerController(IMapper mapper,
                                  ICustomerRepository customerRepository,
                                  ICustomerService customerService,
                                  INotifier notificator) : base(notificator)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerViewModel>> GetById(Guid id)
        {
            var supplier = await GetCustomerById(id);

            if (supplier == null) return NotFound();

            return supplier;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CustomerViewModel>> Create(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _customerService.CreateAsync(_mapper.Map<Customer>(customerViewModel));

            return CustomResponse(HttpStatusCode.Created, customerViewModel);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<CustomerViewModel>> Update(Guid id, CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _customerService.UpdateAsync(_mapper.Map<Customer>(customerViewModel));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<CustomerViewModel>> Delete(Guid id)
        {
            await _customerService.RemoveAsync(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }
        private async Task<CustomerViewModel> GetCustomerById(Guid id)
        {
            return _mapper.Map<CustomerViewModel>(await _customerRepository.GetByIdAsync(id));
        }
    }
}