using AutoMapper;
using E_CommerceSystem.Models;


public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Category
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<CategoryCreateDTO, Category>();

        // Product
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<ProductDTO, Product>();
    }
}
