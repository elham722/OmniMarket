
using Microsoft.AspNetCore.Authorization;
using OmniMarket.Application.Exceptions;

namespace OmniMarket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController(IMediator mediator, IMapper mapper) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
        {
            var command = mapper.Map<CreateProductCommand>(dto);
            var response = await mediator.Send(command, cancellationToken);

            if (!response.Success)
            {
                var apiResponse = ApiResponse<object>.Error(response.Message, string.Join("; ", response.Errors));
                return BadRequest(apiResponse);
            }

            var apiResponseSuccess = ApiResponse<Guid>.Success(response.Id, response.Message);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, apiResponseSuccess);
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
            var command = mapper.Map<UpdateProductCommand>(dto);
            command.Id = id;
            var response = await mediator.Send(command, cancellationToken);

            if (!response.Success)
            {
                var apiResponse = ApiResponse<object>.Error(response.Message, string.Join("; ", response.Errors));
                return BadRequest(apiResponse);
            }

            var apiResponseSuccess = ApiResponse<Guid>.Success(response.Id, response.Message);
            return Ok(apiResponseSuccess);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand { Id = id };
            var response = await mediator.Send(command, cancellationToken);

            if (!response.Success)
            {
                var apiResponse = ApiResponse<object>.Error(response.Message, string.Join("; ", response.Errors));
                return NotFound(apiResponse);
            }

            var apiResponseSuccess = ApiResponse<object>.Success(null, response.Message);
            return Ok(apiResponseSuccess);
        }
    }
}