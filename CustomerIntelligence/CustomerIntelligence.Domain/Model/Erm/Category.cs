﻿namespace NuClear.CustomerIntelligence.Domain.Model.Erm
{
    public sealed class Category : IErmObject
    {
        public Category()
        {
            IsActive = true;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public long? ParentId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}