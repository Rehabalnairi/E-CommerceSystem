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

        // Supplier
        CreateMap<Supplier, SupplierDTO>().ReverseMap();
        CreateMap<SupplierDTO, Supplier>();

        CreateMap<Order, OrderSummaryDTO>()
        .ForMember(dest => dest.OID, opt => opt.MapFrom(src => src.OID))
        .ForMember(dest => dest.UName, opt => opt.MapFrom(src => src.user.UName))
        .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderProducts));

        CreateMap<OrderProducts, OrderItemDTO>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.PID))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.product.ProductName))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

    }
}

