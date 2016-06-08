﻿using System;

using Nancy.Hosting.Self;

using NuClear.Jobs.Schedulers;

using Topshelf;

namespace NuClear.River.Hosting.Interactive
{
    public sealed class Host
    {
        private readonly HostParameters _parameters;
        private readonly ISchedulerManager _schedulerManager;

        public Host(HostParameters parameters, ISchedulerManager schedulerManager)
        {
            _parameters = parameters;
            _schedulerManager = schedulerManager;
        }

        public void ConfigureAndRun()
        {
            HostFactory.Run(
                config =>
                    {
                        NancyHost nancyHost = null;
                        config.Service<ISchedulerManager>(
                            service =>
                                {
                                    service.ConstructUsing(_ => _schedulerManager);

                                    service.WhenStarted(
                                        (manager, hostControl) =>
                                            {
                                                nancyHost = new NancyHost(
                                                    new Uri("http://localhost:5000"),
                                                    new Bootstrapper(new InteractiveModule(_parameters.UpdateServerUrl, hostControl)));

                                                manager.Start();
                                                nancyHost.Start();

                                                return true;
                                            });

                                    service.WhenStopped(
                                        manager =>
                                            {
                                                manager.Stop();
                                                nancyHost?.Stop();
                                            });

                                    service.AfterStoppingService(_ => nancyHost?.Dispose());
                                });

                        config.SetServiceName(_parameters.HostName);
                        config.SetDisplayName(_parameters.HostDisplayName);
                        config.EnableShutdown();

                        config.AddCommandLineSwitch("squirrel", _ => { });
                        config.AddCommandLineDefinition("firstrun",
                                                        _ =>
                                                            {
                                                                config.ApplyCommandLine("install");
                                                                config.ApplyCommandLine("start");
                                                            });
                        config.AddCommandLineDefinition("updated",
                                                        _ =>
                                                        {
                                                            config.ApplyCommandLine("install");
                                                            config.ApplyCommandLine("start");
                                                        });
                        config.AddCommandLineDefinition("obsolete",
                                                        _ =>
                                                        {
                                                            config.ApplyCommandLine("stop");
                                                            config.ApplyCommandLine("uninstall");
                                                        });
                        config.AddCommandLineDefinition("install", _ => { Environment.Exit(0); });
                        config.AddCommandLineDefinition("uninstall", _ => { Environment.Exit(0); });
                    });
        }
    }
}