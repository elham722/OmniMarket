
namespace OmniMarket.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
        }
    }
}
