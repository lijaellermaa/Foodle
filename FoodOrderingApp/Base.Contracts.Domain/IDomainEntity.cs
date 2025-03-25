namespace Base.Contracts.Domain;

public interface IDomainEntity : IDomainEntity<Guid>
{
}
    
public interface IDomainEntity<TKey> : IDomainEntityId<TKey>, IDomainEntityMetadata
    where TKey : struct, IEquatable<TKey>
{
}