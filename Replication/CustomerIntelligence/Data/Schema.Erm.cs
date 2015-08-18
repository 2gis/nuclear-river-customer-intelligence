using System;

using LinqToDB.Mapping;

using NuClear.AdvancedSearch.Replication.CustomerIntelligence.Model.Erm;

namespace NuClear.AdvancedSearch.Replication.CustomerIntelligence.Data
{
    public static partial class Schema
    {
        private const string BillingSchema = "Billing";
        private const string BusinessDirectorySchema = "BusinessDirectory";
        private const string ActivitySchema = "Activity";

        public static MappingSchema Erm
        {
            get
            {
                var schema = new MappingSchema();

                // NOTE: ERM ������ ���� � UTC � �������������� ���� datetime, ������ ��������������� �������� UTC ����
                schema.SetConvertExpression<DateTime, DateTime>(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
                schema.SetConvertExpression<DateTime, DateTimeOffset>(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));

                var config = schema.GetFluentMappingBuilder();

                config.HasAttribute<Account>(new TableAttribute { Schema = BillingSchema, Name = "Accounts", IsColumnAttributeRequired = false });
                config.HasAttribute<BranchOfficeOrganizationUnit>(new TableAttribute { Schema = BillingSchema, Name = "BranchOfficeOrganizationUnits", IsColumnAttributeRequired = false });
                config.HasAttribute<Category>(new TableAttribute { Schema = BusinessDirectorySchema, Name = "Categories", IsColumnAttributeRequired = false });
                config.HasAttribute<CategoryGroup>(new TableAttribute { Schema = BusinessDirectorySchema, Name = "CategoryGroups", IsColumnAttributeRequired = false });
                config.HasAttribute<CategoryFirmAddress>(new TableAttribute { Schema = BusinessDirectorySchema, Name = "CategoryFirmAddresses", IsColumnAttributeRequired = false });
                config.HasAttribute<CategoryOrganizationUnit>(new TableAttribute { Schema = BusinessDirectorySchema, Name = "CategoryOrganizationUnits", IsColumnAttributeRequired = false });
                config.HasAttribute<Client>(new TableAttribute { Schema = BillingSchema, Name = "Clients", IsColumnAttributeRequired = false });
                config.HasAttribute<Contact>(new TableAttribute { Schema = BillingSchema, Name = "Contacts", IsColumnAttributeRequired = false });
                config.HasAttribute<Firm>(new TableAttribute { Schema = BusinessDirectorySchema, Name = "Firms", IsColumnAttributeRequired = false });
                config.HasAttribute<FirmAddress>(new TableAttribute { Schema = BusinessDirectorySchema, Name = "FirmAddresses", IsColumnAttributeRequired = false });
                config.HasAttribute<FirmContact>(new TableAttribute { Schema = BusinessDirectorySchema, Name = "FirmContacts", IsColumnAttributeRequired = false });
                config.HasAttribute<LegalPerson>(new TableAttribute { Schema = BillingSchema, Name = "LegalPersons", IsColumnAttributeRequired = false });
                config.HasAttribute<Order>(new TableAttribute { Schema = BillingSchema, Name = "Orders", IsColumnAttributeRequired = false });
                config.HasAttribute<Project>(new TableAttribute { Schema = BillingSchema, Name = "Projects", IsColumnAttributeRequired = false });
                config.HasAttribute<Territory>(new TableAttribute { Schema = BusinessDirectorySchema, Name = "Territories", IsColumnAttributeRequired = false });

                config.Entity<Account>().HasSchemaName(BillingSchema).HasTableName("Accounts").Property(x => x.Id).IsPrimaryKey();
                config.Entity<BranchOfficeOrganizationUnit>().HasSchemaName(BillingSchema).HasTableName("BranchOfficeOrganizationUnits").Property(x => x.Id).IsPrimaryKey();
                config.Entity<Category>().HasSchemaName(BusinessDirectorySchema).HasTableName("Categories").Property(x => x.Id).IsPrimaryKey();
                config.Entity<CategoryGroup>().HasSchemaName(BusinessDirectorySchema).HasTableName("CategoryGroup")
                      .Property(x => x.Id).IsPrimaryKey()
                      .Property(x => x.Rate).HasColumnName("GroupRate");
                config.Entity<CategoryFirmAddress>().HasSchemaName(BusinessDirectorySchema).HasTableName("CategoryFirmAddresses").Property(x => x.Id).IsPrimaryKey();
                config.Entity<CategoryOrganizationUnit>().HasSchemaName(BusinessDirectorySchema).HasTableName("CategoryOrganizationUnits").Property(x => x.Id).IsPrimaryKey();
                config.Entity<Client>().HasSchemaName(BillingSchema).HasTableName("Clients").Property(x => x.Id).IsPrimaryKey();
                config.Entity<Contact>()
                      .HasSchemaName(BillingSchema)
                      .HasTableName("Contacts")
                      .Property(x => x.Id).IsPrimaryKey()
                      .Property(x => x.Role).HasColumnName("AccountRole");
                config.Entity<Firm>()
                      .HasSchemaName(BusinessDirectorySchema)
                      .HasTableName("Firms")
                      .Property(x => x.Id).IsPrimaryKey()
                      .Property(x => x.OwnerId).HasColumnName("OwnerCode");
                config.Entity<FirmAddress>().HasSchemaName(BusinessDirectorySchema).HasTableName("FirmAddresses").Property(x => x.Id).IsPrimaryKey();
                config.Entity<FirmContact>().HasSchemaName(BusinessDirectorySchema).HasTableName("FirmContacts").Property(x => x.Id).IsPrimaryKey();
                config.Entity<LegalPerson>().HasSchemaName(BillingSchema).HasTableName("LegalPersons").Property(x => x.Id).IsPrimaryKey();
                config.Entity<Order>().HasSchemaName(BillingSchema).HasTableName("Orders").Property(x => x.Id).IsPrimaryKey();
                config.Entity<Project>().HasSchemaName(BillingSchema).HasTableName("Projects")
                      .Property(x => x.Id).IsPrimaryKey()
                      .Property(x => x.Name).HasColumnName("DisplayName");
                config.Entity<Territory>().HasSchemaName(BusinessDirectorySchema).HasTableName("Territories").Property(x => x.Id).IsPrimaryKey();

                config.Entity<Appointment>()
                      .HasSchemaName(ActivitySchema).HasTableName("AppointmentBase")
                      .Property(x => x.Id).IsPrimaryKey();
                config.Entity<Letter>()
                      .HasSchemaName(ActivitySchema).HasTableName("LetterBase")
                      .Property(x => x.Id).IsPrimaryKey();
                config.Entity<Phonecall>()
                      .HasSchemaName(ActivitySchema).HasTableName("PhonecallBase")
                      .Property(x => x.Id).IsPrimaryKey();
                config.Entity<Task>()
                      .HasSchemaName(ActivitySchema).HasTableName("TaskBase")
                      .Property(x => x.Id).IsPrimaryKey();

                config.Entity<AppointmentReference>()
                      .HasSchemaName(ActivitySchema).HasTableName("AppointmentReferences")
                      .Property(x => x.ActivityId).HasColumnName("AppointmentId").IsPrimaryKey()
                      .Property(x => x.Reference).IsPrimaryKey()
                      .Property(x => x.ReferencedObjectId).IsPrimaryKey()
                      .Property(x => x.ReferencedType).IsPrimaryKey();
                config.Entity<LetterReference>()
                      .HasSchemaName(ActivitySchema).HasTableName("LetterReferences")
                      .Property(x => x.ActivityId).HasColumnName("LetterId").IsPrimaryKey()
                      .Property(x => x.Reference).IsPrimaryKey()
                      .Property(x => x.ReferencedObjectId).IsPrimaryKey()
                      .Property(x => x.ReferencedType).IsPrimaryKey();
                config.Entity<PhonecallReference>()
                      .HasSchemaName(ActivitySchema).HasTableName("PhonecallReferences")
                      .Property(x => x.ActivityId).HasColumnName("PhonecallId").IsPrimaryKey()
                      .Property(x => x.Reference).IsPrimaryKey()
                      .Property(x => x.ReferencedObjectId).IsPrimaryKey()
                      .Property(x => x.ReferencedType).IsPrimaryKey();
                config.Entity<TaskReference>()
                      .HasSchemaName(ActivitySchema).HasTableName("TaskReferences")
                      .Property(x => x.ActivityId).HasColumnName("TaskId").IsPrimaryKey()
                      .Property(x => x.Reference).IsPrimaryKey()
                      .Property(x => x.ReferencedObjectId).IsPrimaryKey()
                      .Property(x => x.ReferencedType).IsPrimaryKey();

                return schema;
            }
        }
    }
}