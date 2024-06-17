using AutoMapper;
using WebStore.Business.Models;
using WebStore.Products.Api.ViewModels;

namespace WebStore.Products.Api.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig() 
        {
            CreateMap<ProductViewModel, Product>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductIdViewModel>();
        }
    }
}
