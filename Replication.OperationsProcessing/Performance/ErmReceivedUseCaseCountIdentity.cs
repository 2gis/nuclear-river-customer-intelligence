using NuClear.Telemetry;

namespace NuClear.Replication.OperationsProcessing.Performance
{
    public sealed class ErmReceivedUseCaseCountIdentity : TelemetryIdentityBase<ErmReceivedUseCaseCountIdentity>
    {
        public override int Id
        {
            get { return 0; }
        }

        public override string Description
        {
            get { return "���������� �������� TUC �� ERM (��������� �� SB)"; }
        }
    }
}