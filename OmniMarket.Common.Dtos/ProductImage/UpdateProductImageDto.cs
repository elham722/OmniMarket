﻿namespace OmniMarket.Common.Dtos.ProductImage
{
    public class UpdateProductImageDto 
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public bool IsPrimary { get; set; }
    }

}
