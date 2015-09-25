using System;
using System.Configuration;
using System.Data.Common;

using NuClear.Metamodeling.Provider;
using NuClear.Querying.Metadata;
using NuClear.Querying.Web.OData.Settings;

namespace NuClear.Querying.Web.OData.DataAccess
{
    public sealed class ODataConnectionFactory
    {
        private readonly IMetadataProvider _metadataProvider;
        private readonly IConnectionStringSettings _connectionStringSettings;

        public ODataConnectionFactory(IMetadataProvider metadataProvider, IConnectionStringSettings connectionStringSettings)
        {
            _metadataProvider = metadataProvider;
            _connectionStringSettings = connectionStringSettings;
        }

        public DbConnection CreateConnection(Uri contextId)
        {
            BoundedContextElement contextElement;
            if (!_metadataProvider.TryGetMetadata(contextId, out contextElement))
            {
                return null;
            }

            // ����� ����� ������������� DbConnection ��������� contextId, �� ���� ��� �� �����
            var commonConnectionString = _connectionStringSettings.GetConnectionStringSettings(ConnectionStringName.CustomerIntelligence);
            return CreateConnection(commonConnectionString);
        }

        private static DbConnection CreateConnection(ConnectionStringSettings connectionStringSettings)
        {
            var dbProviderFactory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
            var dbConection = dbProviderFactory.CreateConnection();
            if (dbConection == null)
            {
                throw new ArgumentException();
            }

            dbConection.ConnectionString = connectionStringSettings.ConnectionString;
            return dbConection;
        }
    }
}