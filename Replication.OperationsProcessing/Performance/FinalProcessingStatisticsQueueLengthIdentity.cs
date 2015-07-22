using NuClear.Telemetry;

namespace NuClear.Replication.OperationsProcessing.Performance
{
    public class FinalProcessingStatisticsQueueLengthIdentity : TelemetryIdentityBase<FinalProcessingStatisticsQueueLengthIdentity>
    {
        public override int Id
        {
            get { return 0; }
        }

        public override string Description
        {
            get { return "������ ������� ETL2 (����������)"; }
        }
    }
}