using System.Linq.Dynamic.Core;

namespace Science.Features.DataLoader.Contracts;

public sealed class Sort
{
    public required string Field { get; init; }
    public string Order { get; init; } = "ASC";

    public static IQueryable<T> Apply<T>(IQueryable<T> query, List<Sort>? sort)
    {
        if (sort == null || sort.Count == 0)
        {
            return query;
        }

        var ordering = string.Join(",", sort.Select(s => $"{s.Field} {s.Order}"));
        return query.OrderBy(ordering);
    }
}

public sealed class GetAllRequest
{
    public required int Limit { get; set; }
    public required int Offset { get; set; }

    public required List<Sort>? Sorts { get; set; }
}
