﻿using NuClear.Model.Common.Operations.Identity;

namespace NuClear.Replication.OperationsProcessing.Metadata.Operations
{
    public sealed class RepairOutdatedIdentity : OperationIdentityBase<RepairOutdatedIdentity>, INonCoupledOperationIdentity
    {
        public override int Id
        {
            get
            {
                return OperationIdentityIds.RepairOutdatedIdentity;
            }
        }

        public override string Description
        {
            get
            {
                return "Актуализировать используемый прайс-лист";
            }
        }
    }
}