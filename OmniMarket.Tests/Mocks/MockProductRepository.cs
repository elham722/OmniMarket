
namespace OmniMarket.Tests.Mocks
{
   public static class MockProductRepository
   {
       public static Mock<Product> GetProductRepository()
       {
           var products = new List<Product>()
           {
               new Product() { Id = new Guid("51CD3B90-1AE6-4FC6-A513-B2E68CADBD2A"),Name = "noting" ,Price = 100,Stock = 20,CreatedDate = DateTime.Now,CreatedById = new Guid()}
           };

           var mockRepo = new Mock<IProductRepository>();

         // mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

         //mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>()))
         //    .ReturnsAsync((Product product) =>
         //    {
         //        products.Add(product);
         //        return product;
         //    });

         //  return mockRepo;
         return null;
       }
      
   }
}
