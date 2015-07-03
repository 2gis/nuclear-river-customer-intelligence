using NuClear.Telemetry;

namespace NuClear.Replication.OperationsProcessing.Performance
{
    public sealed class ErmFactOperationProcessedCountIdentity : TelemetryIdentityBase<ErmFactOperationProcessedCountIdentity>
    {
        public override int Id
        {
            get { return 0; }
        }

        public override string Description
        {
            get { return "���������� ������������ �������� �� ����� primary"; }
        }
    }
}