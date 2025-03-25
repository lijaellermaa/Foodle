namespace Base.Contracts.Domain;

public interface IDomainEntityAppUser : IDomainEntityAppUser<Guid>
{
}

public interface IDomainEntityAppUser<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey? AppUserId { get; set; }
}