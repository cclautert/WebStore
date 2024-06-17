using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Products.Api.ViewModels;

namespace WebStore.Products.Api.Controllers
{
    [Route("api/products")]
    public class ProductController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository,
                                 IProductService productService,
                                 IMapper mapper,
                                 INotifier notificator) : base(notificator)
        {
            _productRepository = productRepository;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductIdViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductIdViewModel>>(await _productRepository.GetAllAsync());                               
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> GetById(Guid id)
        {
            var productViewModel = await GetProductById(id);

            if (productViewModel == null) return NotFound();

            return productViewModel;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> Create(ProductViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _productService.CreateAsync(_mapper.Map<Product>(produtoViewModel));

            return CustomResponse(HttpStatusCode.Created ,produtoViewModel);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, ProductViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var productUpdated = await GetProductById(id);
            
            productUpdated.Name = produtoViewModel.Name;
            productUpdated.Description = produtoViewModel.Description;
            productUpdated.Value = produtoViewModel.Value;
            productUpdated.DateRegister = produtoViewModel.DateRegister;

            await _productService.UpdateAsync(_mapper.Map<Product>(productUpdated));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> Delete(Guid id)
        {
            var product = await GetProductById(id);

            if (product == null) return NotFound();

            await _productService.RemoveAsync(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        private async Task<ProductViewModel> GetProductById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));
        }
    }
}