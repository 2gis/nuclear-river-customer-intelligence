﻿using NuClear.AdvancedSearch.Settings;
using NuClear.OperationsLogging.Transports.ServiceBus;
using NuClear.Settings.API;

namespace NuClear.AdvancedSearch.Replication.EntryPoint.Settings
{
    public sealed class ReplicationServiceSettings : SettingsContainerBase
    {
        public ReplicationServiceSettings()
        {
            var connectionStringSettings = new ConnectionStringsSettingsAspect();

            Aspects.Use<EnvironmentSettingsAspect>();
            Aspects.Use(new PersistentStoreAspect(connectionStringSettings));
            Aspects.Use(new ServiceBusReceiverSettingsAspect(connectionStringSettings.GetConnectionString(ConnectionStringName.ServiceBus)));
            Aspects.Use<TaskServiceProcessingSettingsAspect>();
            Aspects.Use(connectionStringSettings);
        }
    }
}