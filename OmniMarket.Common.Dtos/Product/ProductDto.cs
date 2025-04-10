﻿
namespace OmniMarket.Common.Dtos.Product
{
   public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<ProductImageDto> ProductImages { get; set; } = new List<ProductImageDto>();
    }
}
