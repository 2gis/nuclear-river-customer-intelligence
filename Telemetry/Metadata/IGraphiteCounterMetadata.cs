namespace NuClear.Telemetry
{
    // TODO {a.rechkalov, 22.06.2015}: MetadataSource �� ERM
    public interface IGraphiteCounterMetadata
    {
        GraphiteMetadataElement Get<T>()
            where T : TelemetryIdentityBase<T>, new();
    }
}