﻿using System;

using NuClear.Metamodeling.Elements.Identities.Builder;
using NuClear.Metamodeling.Kinds;

using MetadataBuilder = NuClear.Metamodeling.Elements.Identities.Builder.Metadata;

namespace NuClear.Replication.Metadata
{
    public class ImportStatisticsMetadataIdentity : MetadataKindIdentityBase<ImportStatisticsMetadataIdentity>
    {
        private readonly Uri _id = MetadataBuilder.Id.For(MetadataBuilder.Id.DefaultRoot, "ImportStatistics");

        public override Uri Id
        {
            get { return _id; }
        }

        public override string Description
        {
            get { return "Statistics import process descriptive metadata"; }
        }
    }
}