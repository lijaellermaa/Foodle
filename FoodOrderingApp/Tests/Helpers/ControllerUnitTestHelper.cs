using AutoMapper;
using Base.Contracts.Domain;

namespace Tests.Helpers;

public class ControllerUnitTestHelper<TProfile>
    where TProfile : Profile, new()
{
    public static IMapper GetMapper() => new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new TProfile())));
    public static T GetEntity<T>(Guid? id = null) where T : IDomainEntityId, new()
    {
        return new T
        {
            Id = id ?? Guid.NewGuid()
        };
    }

    public static IEnumerable<T> GetEntities<T>(int count)
        where T : IDomainEntityId, new()
    {
        return Enumerable
            .Range(0, count)
            .Select(_ => GetEntity<T>());
    }

    public static T AddId<T>(T entity, Guid? guid = null) where T : class, IDomainEntityId
    {
        // Create a new instance of T using Activator.CreateInstance<T>()
        // This requires that T has a parameterless constructor.
        var newEntity = Activator.CreateInstance<T>();

        // Set the Id property of the new instance to a new Guid
        var idProperty = typeof(T).GetProperty(nameof(entity.Id));
        idProperty?.SetValue(newEntity, guid ?? Guid.NewGuid());

        // Copy other properties from the original entity to the new entity
        // This assumes all properties are of the same type and can be copied directly.
        // If T has properties that cannot be copied directly (e.g., read-only properties), you'll need to handle them separately.
        foreach (var property in typeof(T).GetProperties())
        {
            if (property.Name != nameof(entity.Id) && property.CanWrite)
            {
                property.SetValue(newEntity, property.GetValue(entity));
            }
        }

        return newEntity;
    }
}