﻿using System;

using NuClear.Replication.Core.Commands;

namespace NuClear.CustomerIntelligence.Domain.Commands
{
    public sealed class CreateDataObjectCommand : CreateDataObjectCommandBase
    {
        public CreateDataObjectCommand(Type dataObjectType, long dataObjectId)
        {
            DataObjectType = dataObjectType;
            DataObjectId = dataObjectId;
        }

        public override Type DataObjectType { get; }
        public long DataObjectId { get; }
    }
}