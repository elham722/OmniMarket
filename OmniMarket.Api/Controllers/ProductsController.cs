
namespace OmniMarket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        
        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
        {
            var command = mapper.Map<CreateProductCommand>(dto);
            var productId = await mediator.Send(command, cancellationToken);
            var response = ApiResponse<Guid>.Success(productId, "محصول با موفقیت ایجاد شد");
            return CreatedAtAction(nameof(GetById), new { id = productId }, response);
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

        // PUT: api/Products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto, CancellationToken cancellationToken)
        {
            var command = mapper.Map<UpdateProductCommand>(dto);
            command.Id = id; // Id رو دستی تنظیم می‌کنیم
            var updatedProductId = await mediator.Send(command, cancellationToken);
            var response = ApiResponse<Guid>.Success(updatedProductId, "محصول با موفقیت به‌روزرسانی شد");
            return Ok(response);
        }

        // DELETE: api/Products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand { Id = id };
            await mediator.Send(command, cancellationToken);
            var response = ApiResponse<object>.Success(null, "محصول با موفقیت حذف شد");
            return Ok(response);
        }
    }
}