using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace Public.DTO.v1;

public class Location : IDomainEntityId
{
    public Guid Id { get; set; }

    public string Area { get; set; } = default!;
    public string Town { get; set; } = default!;
    public string Address { get; set; } = default!;
}

public class LocationRequest
{
    public Guid? Id { get; set; }

    [MaxLength(256)]
    [MinLength(1)]
    public string Area { get; set; } = default!;

    [MaxLength(256)]
    [MinLength(1)]
    public string Town { get; set; } = default!;

    [MaxLength(256)]
    [MinLength(1)]
    public string Address { get; set; } = default!;
}