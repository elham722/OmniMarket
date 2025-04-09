using OmniMarket.Web.Contracts;
using OmniMarket.Web.ViewModels.Product;

namespace OmniMarket.Web.Controllers
{
    [Authorize]
    public class ProductsController(IProductService productService) : Controller
    {
        private readonly IProductService _productService = productService;

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAllProductsAsync();
            if (!response.Status)
            {
                TempData["ErrorMessage"] = response.Message;
                if (!string.IsNullOrEmpty(response.ValidationErrors))
                {
                    TempData["ValidationErrors"] = response.ValidationErrors;
                }
                return View(new List<ProductViewModel>());
            }
            return View(response.Data);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            if (!response.Status)
            {
                TempData["ErrorMessage"] = response.Message;
                if (!string.IsNullOrEmpty(response.ValidationErrors))
                {
                    TempData["ValidationErrors"] = response.ValidationErrors;
                }
                return NotFound();
            }
            return View(response.Data);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View(new CreateProductViewModel());
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync(productViewModel);
                if (!response.Status)
                {
                    TempData["ErrorMessage"] = response.Message;
                    if (!string.IsNullOrEmpty(response.ValidationErrors))
                    {
                        TempData["ValidationErrors"] = response.ValidationErrors;
                    }
                    return View(productViewModel);
                }
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            if (!response.Status)
            {
                TempData["ErrorMessage"] = response.Message;
                if (!string.IsNullOrEmpty(response.ValidationErrors))
                {
                    TempData["ValidationErrors"] = response.ValidationErrors;
                }
                return NotFound();
            }

            var productViewModel = new UpdateProductViewModel
            {
                Name = response.Data.Name,
                Description = response.Data.Description,
                Price = response.Data.Price,
                Stock = response.Data.Stock,
                ProductImages = response.Data.ProductImages.Select(i => new UpdateProductImageViewModel
                {
                    Id = i.Id,
                    Url = i.Url,
                    IsPrimary = i.IsPrimary
                }).ToList()
            };
            return View(productViewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync(id, productViewModel);
                if (!response.Status)
                {
                    TempData["ErrorMessage"] = response.Message;
                    if (!string.IsNullOrEmpty(response.ValidationErrors))
                    {
                        TempData["ValidationErrors"] = response.ValidationErrors;
                    }
                    return View(productViewModel);
                }
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _productService.DeleteProductAsync(id);
            if (!response.Status)
            {
                TempData["ErrorMessage"] = response.Message;
                if (!string.IsNullOrEmpty(response.ValidationErrors))
                {
                    TempData["ValidationErrors"] = response.ValidationErrors;
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = response.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}