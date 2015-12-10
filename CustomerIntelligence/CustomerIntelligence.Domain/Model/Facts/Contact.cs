﻿namespace NuClear.CustomerIntelligence.Domain.Model.Facts
{
    public sealed class Contact : IErmFactObject
    {
        public long Id { get; set; }

        public int Role { get; set; }

        public bool HasPhone { get; set; }

        public bool HasWebsite { get; set; }

        public long ClientId { get; set; }
    }
}