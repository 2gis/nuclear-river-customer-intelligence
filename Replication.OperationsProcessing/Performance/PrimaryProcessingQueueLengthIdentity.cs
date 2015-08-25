using NuClear.Telemetry;

namespace NuClear.Replication.OperationsProcessing.Performance
{
    public class PrimaryProcessingQueueLengthIdentity : TelemetryIdentityBase<PrimaryProcessingQueueLengthIdentity>
    {
        public override int Id
        {
            get { return 0; }
        }

        public override string Description
        {
            get { return "������ ������� ETL1 (����� TUC � SB)"; }
        }
    }
}