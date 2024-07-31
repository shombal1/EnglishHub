namespace EnglishHub.Domain.monitoring;

public class TagsBuilder
{
    private IDictionary<string, object?> _tags = new Dictionary<string, object?>();

    public TagsBuilder AddSuccess(bool success)
    {
        _tags["success"] = success;
        return this;
    }

    public ReadOnlySpan<KeyValuePair<string, object?>> Build()
    {
        return _tags.ToArray();
    }
}