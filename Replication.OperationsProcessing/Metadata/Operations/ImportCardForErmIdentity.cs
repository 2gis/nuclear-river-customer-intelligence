﻿using NuClear.Model.Common.Operations.Identity;

namespace NuClear.Replication.OperationsProcessing.Metadata.Operations
{
    public sealed class ImportCardForErmIdentity : OperationIdentityBase<ImportCardForErmIdentity>, INonCoupledOperationIdentity
    {
        public override int Id
        {
            get { return OperationIdentityIds.ImportCardForErmIdentity; }
        }

        public override string Description
        {
            get { return "Импорт сообщеиня flowCardsForERM.CardForERM"; }
        }
    }
}