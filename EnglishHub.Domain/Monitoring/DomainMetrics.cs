using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace EnglishHub.Domain.monitoring;

public class DomainMetrics
{
    private readonly Meter _meter = new Meter("English.Hub.Domain");
    private readonly ConcurrentDictionary<string, Counter<long>> _counters=new();
    
    public void IncrementCount(string name, long value, ReadOnlySpan<KeyValuePair<string, object?>> tags)
    {
        Counter<long> counter = _counters.GetOrAdd(name,_=> _meter.CreateCounter<long>(name));
        counter.Add(value,tags);
    }
}