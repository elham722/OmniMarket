using OmniMarket.Web.Contracts;
using OmniMarket.Web.Models.Product;
using OmniMarket.Web.Services.Base;

namespace OmniMarket.Web.Services
{
    public class ProductService(IClient client, ILocalStorageService localStorage, IMapper mapper) : BaseHttpService(client, localStorage), IProductService
    {
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<List<ProductViewModel>>> GetAllProductsAsync()
        {
            try
            {
                AddBearerToken();
                var response = await client.GetAllProductsAsync();
                var result = new ApiResponse<List<ProductViewModel>>
                {
                    Status = response.Status,
                    Message = response.Message,
                    ValidationErrors = response.ValidationErrors,
                    Data = response.Status ? _mapper.Map<List<ProductViewModel>>(response.Data) : null
                };
                return result;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<List<ProductViewModel>>(ex);
            }
        }

        public async Task<ApiResponse<ProductViewModel>> GetProductByIdAsync(Guid id)
        {
            try
            {
                AddBearerToken();
                var response = await client.GetProductByIdAsync(id);
                var result = new ApiResponse<ProductViewModel>
                {
                    Status = response.Status,
                    Message = response.Message,
                    ValidationErrors = response.ValidationErrors,
                    Data = response.Status ? _mapper.Map<ProductViewModel>(response.Data) : null
                };
                return result;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<ProductViewModel>(ex);
            }
        }

        public async Task<ApiResponse<Guid>> CreateProductAsync(CreateProductViewModel product)
        {
            try
            {
              
                var productDto = _mapper.Map<CreateProductDto>(product);

                AddBearerToken();

                return await client.CreateProductAsync(productDto);
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<ApiResponse<Guid>> UpdateProductAsync(Guid id, UpdateProductViewModel product)
        {
            try
            {
                AddBearerToken();
                var productDto = _mapper.Map<UpdateProductDto>(product);
                return await client.UpdateProductAsync(id, productDto);
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<ApiResponse<object>> DeleteProductAsync(Guid id)
        {
            try
            {
                AddBearerToken();
                return await client.DeleteProductAsync(id);
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<object>(ex);
            }
        }
    }
}