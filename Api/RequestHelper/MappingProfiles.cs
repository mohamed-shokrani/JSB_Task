using Api.Data;
using Api.Dto_s.OrderDto;
using Api.Dto_s;
using Api.Entities;
using AutoMapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderDetailsDto, opt => opt.MapFrom(src => src.OrderDetails));

        CreateMap<OrderDetails, OrderDetailDto>().ForMember(x=>x.ProductName ,src=>src.MapFrom(x=>x.Product.Name));

        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetailsDto));

        CreateMap<OrderDetailDto, OrderDetails>()
            .ForMember(dest => dest.Order, opt => opt.Ignore());
    }
}
