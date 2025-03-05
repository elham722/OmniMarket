
namespace OmniMarket.UI.Contracts
{
    public interface IProductService
    {
        Task<ApiResponse<List<ProductViewModel>>> GetAllProductsAsync();
        Task<ApiResponse<ProductViewModel>> GetProductByIdAsync(Guid id);
        Task<ApiResponse<Guid>> CreateProductAsync(CreateProductViewModel product);
        Task<ApiResponse<Guid>> UpdateProductAsync(Guid id, UpdateProductViewModel product);
        Task<ApiResponse<object>> DeleteProductAsync(Guid id);
    }
}