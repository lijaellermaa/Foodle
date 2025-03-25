using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace Public.DTO.v1;

public class ProductType : IDomainEntityId
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

public class ProductTypeRequest
{
    public Guid? Id { get; set; }

    [MaxLength(45)]
    [MinLength(1)]
    public string Name { get; set; } = default!;
}