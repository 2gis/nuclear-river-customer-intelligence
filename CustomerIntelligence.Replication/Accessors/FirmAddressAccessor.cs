﻿using System;
using System.Collections.Generic;
using System.Linq;

using NuClear.CustomerIntelligence.Replication.Commands;
using NuClear.CustomerIntelligence.Replication.Events;
using NuClear.CustomerIntelligence.Replication.Specifications;
using NuClear.CustomerIntelligence.Storage.Model.Bit;
using NuClear.CustomerIntelligence.Storage.Model.Facts;
using NuClear.Replication.Core;
using NuClear.Replication.Core.DataObjects;
using NuClear.Storage.API.Readings;
using NuClear.Storage.API.Specifications;

namespace NuClear.CustomerIntelligence.Replication.Accessors
{
    public sealed class FirmAddressAccessor : IStorageBasedDataObjectAccessor<FirmAddress>, IDataChangesHandler<FirmAddress>
    {
        private readonly IQuery _query;

        public FirmAddressAccessor(IQuery query)
        {
            _query = query;
        }

        public IQueryable<FirmAddress> GetSource() => Specs.Map.Erm.ToFacts.FirmAddresses.Map(_query);

        public FindSpecification<FirmAddress> GetFindSpecification(IReadOnlyCollection<ICommand> commands)
        {
            var ids = commands.Cast<SyncDataObjectCommand>().Select(c => c.DataObjectId).ToArray();
            return new FindSpecification<FirmAddress>(x => ids.Contains(x.Id));
        }

        public IReadOnlyCollection<IEvent> HandleCreates(IReadOnlyCollection<FirmAddress> dataObjects) => Array.Empty<IEvent>();

        public IReadOnlyCollection<IEvent> HandleUpdates(IReadOnlyCollection<FirmAddress> dataObjects) => Array.Empty<IEvent>();

        public IReadOnlyCollection<IEvent> HandleDeletes(IReadOnlyCollection<FirmAddress> dataObjects) => Array.Empty<IEvent>();

        public IReadOnlyCollection<IEvent> HandleRelates(IReadOnlyCollection<FirmAddress> dataObjects)
        {
            var ids = dataObjects.Select(x => x.Id).ToArray();
            var specification = new FindSpecification<FirmAddress>(x => ids.Contains(x.Id));

            var complexIds = (from firm in _query.For<Firm>()
                              join project in _query.For<Project>() on firm.OrganizationUnitId equals project.OrganizationUnitId
                              join firmAddress in _query.For(specification) on firm.Id equals firmAddress.FirmId
                              join firmAddressCategory in _query.For<CategoryFirmAddress>() on firmAddress.Id equals firmAddressCategory.FirmAddressId
                              select new StatisticsKey { ProjectId = project.Id, CategoryId = firmAddressCategory.CategoryId })
                .ToArray();

            var firmIds = (from firmAddress in _query.For(specification)
                           select firmAddress.FirmId)
                .ToArray();

            var clientIds = (from firmAddress in _query.For(specification)
                             join firm in _query.For<Firm>() on firmAddress.FirmId equals firm.Id
                             where firm.ClientId.HasValue
                             select firm.ClientId.Value)
                .ToArray();

            return Enumerable.Empty<IEvent>()
                             .Concat(complexIds.Select(x => new RelatedDataObjectOutdatedEvent<StatisticsKey>(typeof(ProjectCategoryStatistics), x)))
                             .Concat(firmIds.Select(x => new RelatedDataObjectOutdatedEvent<long>(typeof(Firm), x)))
                             .Concat(clientIds.Select(x => new RelatedDataObjectOutdatedEvent<long>(typeof(Client), x)))
                             .ToArray();
        }
    }
}