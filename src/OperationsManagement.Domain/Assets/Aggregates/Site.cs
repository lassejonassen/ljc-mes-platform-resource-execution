namespace OperationsManagement.Domain.Assets.Aggregates;

public sealed class Site : AggregateRoot
{
    private Site() { }
    private Site(DateTime utcNow) : base(utcNow) { }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    // Sites track their Areas
    private readonly List<Guid> _areaIds = [];
    public IReadOnlyCollection<Guid> AreaIds => _areaIds.AsReadOnly();

    public static Result<Site> Create(string name, string? description, DateTime utcNow)
    {

        var site = new Site(utcNow)
        {
            Name = name,
            Description = description
        };

        return Result.Success(site);
    }

    public Result Update(string name, string? description)
    {
        Name = name;
        Description = description;

        return Result.Success();
    }
}
