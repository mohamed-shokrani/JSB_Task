using Api.Dto_s;
using Api.Entities;
using AutoMapper;

namespace Api.RequestHelper;

public class MappingProfiles :Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();

    }
}
