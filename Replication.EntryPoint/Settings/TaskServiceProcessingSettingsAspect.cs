﻿using NuClear.Jobs.Settings;
using NuClear.Settings;
using NuClear.Settings.API;

namespace NuClear.AdvancedSearch.Replication.EntryPoint.Settings
{
    public sealed class TaskServiceProcessingSettingsAspect : ISettingsAspect, ITaskServiceProcessingSettings
    {
        private readonly IntSetting _maxWorkingThreads = ConfigFileSetting.Int.Required("MaxWorkingThreads");
        private readonly EnumSetting<JobStoreType> _jobStoreType = ConfigFileSetting.Enum.Required<JobStoreType>("JobStoreType");
        private readonly StringSetting _schedulerName = ConfigFileSetting.String.Required("SchedulerName");

        int ITaskServiceProcessingSettings.MaxWorkingThreads
        {
            get { return _maxWorkingThreads.Value; }
        }

        JobStoreType ITaskServiceProcessingSettings.JobStoreType
        {
            get { return _jobStoreType.Value; }
        }

        string ITaskServiceProcessingSettings.SchedulerName
        {
            get { return _schedulerName.Value; }
        }
    }
}
