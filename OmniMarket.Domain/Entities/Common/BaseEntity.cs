﻿namespace OmniMarket.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
        public bool IsActive { get; set; } = true;


       // public abstract void Validate();
    }
}
