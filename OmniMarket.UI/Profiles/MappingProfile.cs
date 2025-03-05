
namespace OmniMarket.UI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Product

            // ProductDto -> ProductViewModel
            CreateMap<ProductDto, ProductViewModel>();
            CreateMap<ProductImageDto, ProductImageViewModel>();

            // CreateProductViewModel -> CreateProductDto
            CreateMap<CreateProductViewModel, CreateProductDto>();
            CreateMap<CreateProductImageViewModel, CreateProductImageDto>();

            // UpdateProductViewModel -> CreateProductDto (برای ویرایش)
            CreateMap<UpdateProductViewModel, UpdateProductDto>();
            CreateMap<UpdateProductImageViewModel, UpdateProductImageDto>();

            // CreateProductDto -> CreateProductViewModel/UpdateProductViewModel
            CreateMap<CreateProductDto, CreateProductViewModel>();
            CreateMap<CreateProductDto, UpdateProductViewModel>();
            CreateMap<CreateProductImageDto, CreateProductImageViewModel>();
            CreateMap<CreateProductImageDto, UpdateProductImageViewModel>();
            #endregion


        }
    }
}
