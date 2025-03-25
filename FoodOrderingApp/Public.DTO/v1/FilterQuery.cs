using Helpers.Base.Entities;

namespace Public.DTO.v1;

public class FilterQuery
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
    public SortDirection SortBy { get; set; } = SortDirection.None;
    public string? SearchQuery { get; set; } = null;
}