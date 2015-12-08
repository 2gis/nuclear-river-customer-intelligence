﻿namespace NuClear.CustomerIntelligence.Domain.Model.Erm
{
    public sealed class LegalPerson : IErmObject
    {
        public LegalPerson()
        {
            IsActive = true;
        }

        public long Id { get; set; }

        public long? ClientId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}