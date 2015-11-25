namespace NuClear.CustomerIntelligence.Domain.Model.Erm
{
    public sealed class BranchOfficeOrganizationUnit : IErmObject
    {
        public BranchOfficeOrganizationUnit()
        {
            IsActive = true;
        }

        public long Id { get; set; }

        public long OrganizationUnitId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}