namespace NuClear.CustomerIntelligence.Domain.Model.CI
{
    public sealed class FirmTerritory : ICustomerIntelligenceObject
    {
        public long FirmId { get; set; }

        public long FirmAddressId { get; set; }

        public long? TerritoryId { get; set; }
    }
}