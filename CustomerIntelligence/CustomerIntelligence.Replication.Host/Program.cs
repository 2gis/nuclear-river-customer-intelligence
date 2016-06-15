﻿using System;

using Microsoft.Practices.Unity;

using NuClear.CustomerIntelligence.Replication.Host.DI;
using NuClear.CustomerIntelligence.Replication.Host.Settings;
using NuClear.Jobs.Schedulers;
using NuClear.River.Hosting.Common.Identities.Connections;
using NuClear.River.Hosting.Common.Settings;
using NuClear.Settings.API;
using NuClear.Storage.API.ConnectionStrings;
using NuClear.Tracing.API;
using NuClear.Tracing.Environment;
using NuClear.Tracing.Log4Net;
using NuClear.Tracing.Log4Net.Config;

namespace NuClear.CustomerIntelligence.Replication.Host
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var settingsContainer = new ReplicationServiceSettings();
            var environmentSettings = settingsContainer.AsSettings<IEnvironmentSettings>();
            var squirrelSettings = settingsContainer.AsSettings<ISquirrelSettings>();
            var connectionStringSettings = settingsContainer.AsSettings<IConnectionStringSettings>();

            var tracerContextEntryProviders =
                    new ITracerContextEntryProvider[]
                    {
                        new TracerContextConstEntryProvider(TracerContextKeys.Required.Environment, environmentSettings.EnvironmentName),
                        new TracerContextConstEntryProvider(TracerContextKeys.Required.EntryPoint, environmentSettings.HostName),
                        new TracerContextConstEntryProvider(TracerContextKeys.Required.EntryPointHost, NetworkInfo.ComputerFQDN),
                        new TracerContextConstEntryProvider(TracerContextKeys.Required.EntryPointInstanceId, Guid.NewGuid().ToString()),
                        new TracerContextSelfHostedEntryProvider(TracerContextKeys.Required.UserAccount)
                    };

            var tracerContextManager = new TracerContextManager(tracerContextEntryProviders);
            var tracer = Log4NetTracerBuilder.Use
                                             .DefaultXmlConfig
                                             .Console
                                             .File("out.txt")
                                             .Logstash(new Uri(connectionStringSettings.GetConnectionString(LoggingConnectionStringIdentity.Instance)))
                                             .Build;

            tracer.Info($"Host started with args: {string.Join(",", args)}");

            IUnityContainer container = null;
            try
            {
                container = Bootstrapper.ConfigureUnity(settingsContainer, tracer, tracerContextManager);
                var scheduleManager = container.Resolve<ISchedulerManager>();

                var host = new River.Hosting.Interactive.Host(squirrelSettings.UpdateServerUrl, scheduleManager);
                host.ConfigureAndRun();
            }
            finally
            {
                container?.Dispose();
            }
        }
    }
}
