﻿using System.Linq;

using NuClear.CustomerIntelligence.Domain.Model.CI;
using NuClear.Storage.API.Readings;
using NuClear.Storage.API.Specifications;

using Facts = NuClear.CustomerIntelligence.Domain.Model.Facts;

namespace NuClear.CustomerIntelligence.Domain.Specifications
{
    using System;

    public static partial class Specs
    {
        public static partial class Map
        {
            public static partial class Facts
            {
                public static partial class ToStatistics
                { 
                    public static readonly MapSpecification<IQuery, IQueryable<FirmCategoryStatistics>> FirmCategoryStatistics =
                        new MapSpecification<IQuery, IQueryable<FirmCategoryStatistics>>(
                            q =>
                            {
                                var firmCounts = q.For<Facts::FirmCategory>()
                                                  .GroupBy(x => new { x.ProjectId, x.CategoryId })
                                                  .Select(x => new { x.Key.ProjectId, x.Key.CategoryId, Count = x.Count() });

                                return from firm in q.For<Facts::FirmCategory>()
                                       from firmStatistics in q.For<Facts::FirmCategoryStatistics>()
                                                               .Where(x => x.FirmId == firm.FirmId && x.CategoryId == firm.CategoryId && x.ProjectId == firm.ProjectId)
                                                               .DefaultIfEmpty(new Facts::FirmCategoryStatistics())
                                       from categoryStatistics in q.For<Facts::ProjectCategoryStatistics>()
                                                                   .Where(x => x.CategoryId == firm.CategoryId && x.ProjectId == firm.ProjectId)
                                                                   .DefaultIfEmpty(new Facts::ProjectCategoryStatistics())
                                       from firmCount in firmCounts.Where(x => x.CategoryId == firm.CategoryId && x.ProjectId == firm.ProjectId)
                                                                   .DefaultIfEmpty()
                                       select new FirmCategoryStatistics
                                       {
                                           ProjectId = firm.ProjectId,
                                           FirmId = firm.FirmId,
                                           CategoryId = firm.CategoryId,
                                           Hits = firmStatistics != null ? firmStatistics.Hits : 0,
                                           Shows = firmStatistics != null ? firmStatistics.Shows : 0,
                                           FirmCount = firmCount.Count,
                                           AdvertisersShare = Math.Min(1, (float)categoryStatistics.AdvertisersCount / firmCount.Count)
                                       };
                            });
                }
            }
        }
    }
}