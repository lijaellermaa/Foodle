using Base.Contracts.Domain;

namespace Base.Domain;

public abstract class DomainEntity : DomainEntity<Guid>
{
}

public abstract class DomainEntity<TKey> : DomainEntityId<TKey>, IDomainEntity<TKey>
    where TKey : struct, IEquatable<TKey>
{
    public string CreatedBy { get; set; } = "system";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string UpdatedBy { get; set; } = "system";
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}