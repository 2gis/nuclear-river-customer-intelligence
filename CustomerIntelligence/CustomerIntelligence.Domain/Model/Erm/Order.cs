﻿using System;

namespace NuClear.CustomerIntelligence.Domain.Model.Erm
{
    public sealed class Order : IErmObject
    {
        public Order()
        {
            IsActive = true;
        }

        public long Id { get; set; }

        public DateTimeOffset EndDistributionDateFact { get; set; }

        public int WorkflowStepId { get; set; }

        public long FirmId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}