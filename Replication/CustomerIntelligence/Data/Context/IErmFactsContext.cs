using System.Linq;

using NuClear.AdvancedSearch.Replication.CustomerIntelligence.Model.Facts;

namespace NuClear.AdvancedSearch.Replication.CustomerIntelligence.Data.Context
{
    public interface IErmFactsContext
    {
        IQueryable<Account> Accounts { get; }

        IQueryable<Category> Categories { get; }

        IQueryable<CategoryGroup> CategoryGroups { get; }

        IQueryable<BranchOfficeOrganizationUnit> BranchOfficeOrganizationUnits { get; }

        IQueryable<CategoryFirmAddress> CategoryFirmAddresses { get; }
        
        IQueryable<CategoryOrganizationUnit> CategoryOrganizationUnits { get; }

        IQueryable<Client> Clients { get; }

        IQueryable<Contact> Contacts { get; }

        IQueryable<Firm> Firms { get; }

        IQueryable<FirmAddress> FirmAddresses { get; }

        IQueryable<FirmContact> FirmContacts { get; }

        IQueryable<LegalPerson> LegalPersons { get; }

        IQueryable<Order> Orders { get; }

        IQueryable<Project> Projects { get; }

        IQueryable<Territory> Territories { get; }
    }
}