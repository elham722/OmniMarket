
using OmniMarket.Application.Exceptions;

namespace OmniMarket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IMediator mediator, IMapper mapper) : ControllerBase
    {

        // OmniMarket.Api/Controllers/ProductsController.cs
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var command = mapper.Map<CreateProductCommand>(dto);
                var productId = await mediator.Send(command, cancellationToken);
                var response = ApiResponse<Guid>.Success(productId, "محصول با موفقیت ایجاد شد");
                return CreatedAtAction(nameof(GetById), new { id = productId }, response);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var validationErrors = string.Join("; ", ex.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
                var response = ApiResponse<object>.Error("خطای اعتبارسنجی", validationErrors);
                return BadRequest(response);
            }
        }

        // GET: api/Products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery { Id = id };
            var product = await mediator.Send(query, cancellationToken);
            if (product == null)
            {
                var errorResponse = new ApiErrorResponse($"محصول با شناسه {id} پیدا نشد");
                return NotFound(errorResponse);
            }
            var response = ApiResponse<ProductDto>.Success(product, "محصول با موفقیت دریافت شد");
            return Ok(response);
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllProductsQuery();
            var products = await mediator.Send(query, cancellationToken);
            var response = ApiResponse<IReadOnlyList<ProductDto>>.Success(products, "لیست محصولات با موفقیت دریافت شد");
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var command = mapper.Map<UpdateProductCommand>(dto);
                command.Id = id;
                var updatedProductId = await mediator.Send(command, cancellationToken);
                var response = ApiResponse<Guid>.Success(updatedProductId, "محصول با موفقیت به‌روزرسانی شد");
                return Ok(response);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var validationErrors = string.Join("; ", ex.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
                var response = ApiResponse<object>.Error("خطای اعتبارسنجی", validationErrors);
                return BadRequest(response);
            }
            catch (NotFoundException ex)
            {
                var response = ApiResponse<object>.Error(ex.Message);
                return NotFound(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteProductCommand { Id = id };
                await mediator.Send(command, cancellationToken);
                var response = ApiResponse<object>.Success(null, "محصول با موفقیت حذف شد");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                var response = ApiResponse<object>.Error(ex.Message);
                return NotFound(response);
            }
        }
    }
}