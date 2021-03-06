﻿namespace NuClear.CustomerIntelligence.Storage.Model.Statistics
{
    public sealed class FirmCategory3
    {
        public long ProjectId { get; set; }

        public long FirmId { get; set; }

        public long CategoryId { get; set; }

        public string Name { get; set; }

        public int Hits { get; set; }

        public int Shows { get; set; }

        public float AdvertisersShare { get; set; }

        public int FirmCount { get; set; }

        public int? ForecastClick { get; set; }

        public decimal? ForecastAmount { get; set; }
    }
}