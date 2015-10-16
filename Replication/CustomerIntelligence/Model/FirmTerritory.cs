using NuClear.AdvancedSearch.Replication.Model;

namespace NuClear.AdvancedSearch.Replication.CustomerIntelligence.Model
{
    public sealed class FirmTerritory : ICustomerIntelligenceObject
    {
        public long FirmId { get; set; }

        public long FirmAddressId { get; set; }

        public long? TerritoryId { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is FirmTerritory && Equals((FirmTerritory)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (FirmId.GetHashCode() * 397) ^ FirmAddressId.GetHashCode();
            }
        }

        private bool Equals(FirmTerritory other)
        {
            return FirmId == other.FirmId && FirmAddressId == other.FirmAddressId;
        }
    }
}