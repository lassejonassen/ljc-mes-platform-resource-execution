namespace OperationsManagement.Domain.Assets.Aggregates;

public sealed class Area : AggregateRoot
{
    private Area() { }
    private Area(DateTime utcNow) : base(utcNow) { }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid SiteId { get; private set; }

    private readonly List<Guid> _processCellIds = [];
    public IReadOnlyCollection<Guid> ProcessCellIds => _processCellIds.AsReadOnly();

    public static Result<Area> Create(string name, string? description,Guid siteId, DateTime utcNow)
    {

        var area = new Area(utcNow)
        {
            Name = name,
            Description = description,
            SiteId = siteId,
        };

        return Result.Success(area);
    }

    public Result Update(string name, string? description)
    {
        Name = name;
        Description = description;

        return Result.Success();
    }
}
