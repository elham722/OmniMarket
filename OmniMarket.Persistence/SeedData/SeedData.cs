
namespace OmniMarket.Persistence.SeedData
{
    public static class SeedData
    {
        public static void Initialize(OmniMarketDbContext context)
        {
            context.Database.EnsureCreated();

           
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "لپ‌تاپ",
                        Description = "لپ‌تاپ با کارایی بالا",
                        Price = 1000m,
                        Stock = 10,
                        CreatedDate = DateTime.UtcNow,
                        ProductImages = new List<ProductImage>
                        {
                            new ProductImage
                            {
                                Id = Guid.NewGuid(),
                                ProductId = Guid.NewGuid(), 
                                Url = "https://example.com/images/laptop1.jpg",
                                IsPrimary = true,
                                CreatedDate = DateTime.UtcNow
                            },
                            new ProductImage
                            {
                                Id = Guid.NewGuid(),
                                ProductId = Guid.NewGuid(),
                                Url = "https://example.com/images/laptop2.jpg",
                                IsPrimary = false,
                                CreatedDate = DateTime.UtcNow
                            }
                        }
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "گوشی موبایل",
                        Description = "گوشی با دوربین حرفه‌ای",
                        Price = 500m,
                        Stock = 20,
                        CreatedDate = DateTime.UtcNow,
                        ProductImages = new List<ProductImage>
                        {
                            new ProductImage
                            {
                                Id = Guid.NewGuid(),
                                ProductId = Guid.NewGuid(), 
                                Url = "https://example.com/images/phone1.jpg",
                                IsPrimary = true,
                                CreatedDate = DateTime.UtcNow
                            }
                        }
                    }
                };

               
                foreach (var product in products)
                {
                    foreach (var image in product.ProductImages)
                    {
                        image.ProductId = product.Id;
                    }
                }

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}