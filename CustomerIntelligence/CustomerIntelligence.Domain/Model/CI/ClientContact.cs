﻿namespace NuClear.CustomerIntelligence.Domain.Model.CI
{
    public sealed class ClientContact : ICustomerIntelligenceObject
    {
        public long ClientId { get; set; }

        public long ContactId { get; set; }

        public int Role { get; set; }
    }
}