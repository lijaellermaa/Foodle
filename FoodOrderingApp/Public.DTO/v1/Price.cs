using Base.Contracts.Domain;

namespace Public.DTO.v1;

public class PricePure : IDomainEntityId
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public decimal? PreviousValue { get; set; }
    public Guid ProductId { get; set; }
    public string? Comment { get; set; }
}

public class Price : PricePure
{
    public Product? Product { get; set; }
}

public class PriceRequest
{
    public Guid? Id { get; set; }
    public decimal Value { get; set; }
    public decimal? PreviousValue { get; set; }
    public Guid ProductId { get; set; }
    public string? Comment { get; set; }
}