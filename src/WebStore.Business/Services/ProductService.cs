using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Business.Models.Validations;

namespace WebStore.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository produtoRepository,
                              INotifier notificador) : base(notificador)
        {
            _productRepository = produtoRepository;
        }

        public async Task CreateAsync(Product product)
        {
            if (!RunValidation(new ProductValidation(), product)) return;

            var ExistingProduct = _productRepository.GetByIdAsync(product.Id);

            if(ExistingProduct != null)
            {
                Notify("Já existe um produto com o ID informado!");
                return;
            }

            await _productRepository.CreateAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            if (!RunValidation(new ProductValidation(), product)) return;

            await _productRepository.UpdateAsync(product);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _productRepository.RemoveAsync(id);
        }
    }
}
