using NuClear.Telemetry;

namespace NuClear.Replication.OperationsProcessing.Performance
{
    public sealed class FinalProcessingDelayIdentity : TelemetryIdentityBase<FinalProcessingDelayIdentity>
    {
        public override int Id
        {
            get { return 0; }
        }

        public override string Description
        {
            get { return "���������� ��������� ��������� �� ���������"; }
        }
    }
}