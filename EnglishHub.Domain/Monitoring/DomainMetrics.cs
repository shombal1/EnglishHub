using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace EnglishHub.Domain.monitoring;

public class DomainMetrics
{
    public const string DomainActivityName = "EnglishHub.Domain";
    public const string DomainMeterName = "EnglishHub.Domain";
    
    public readonly ActivitySource ActivitySource = new(DomainActivityName);
    
    private readonly Meter _meter;
    private readonly ConcurrentDictionary<string, Counter<long>> _counters=new();

    public DomainMetrics(IMeterFactory meterFactory)
    {
        _meter = meterFactory.Create(DomainMeterName);
    }
    
    public void IncrementCount(string name, long value, ReadOnlySpan<KeyValuePair<string, object?>> tags)
    {
        Counter<long> counter = _counters.GetOrAdd(name,_=> _meter.CreateCounter<long>(name));
        counter.Add(value,tags);
    }
}