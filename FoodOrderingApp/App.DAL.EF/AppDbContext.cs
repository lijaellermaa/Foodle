using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>,
        AppUserRole,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>(options)
{
    public DbSet<Location> Locations { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderItem> OrderItems { get; set; } = default!;
    public DbSet<Price> Prices { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductType> ProductTypes { get; set; } = default!;
    public DbSet<Restaurant> Restaurants { get; set; } = default!;
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder
            .Entity<Order>()
            .Property(e => e.PaymentMethod)
            .HasConversion<string>();
        builder
            .Entity<Order>()
            .Property(e => e.Status)
            .HasConversion<string>();
        builder
            .Entity<Order>()
            .Property(e => e.DeliveryType)
            .HasConversion<string>();

        builder
            .Entity<Order>()
            .Navigation(x => x.Restaurant)
            .AutoInclude();
        builder
            .Entity<Order>()
            .Navigation(x => x.AppUser)
            .AutoInclude();
        builder
            .Entity<Order>()
            .Navigation(x => x.OrderItems)
            .AutoInclude();

        builder
            .Entity<Product>()
            .Navigation(x => x.OrderItems)
            .AutoInclude();
        builder
            .Entity<Product>()
            .Navigation(x => x.ProductType)
            .AutoInclude();

        builder
            .Entity<Price>()
            .HasOne<Product>(x => x.Product)
            .WithMany(x => x.Prices)
            .HasForeignKey(x => x.ProductId);

        builder
            .Entity<OrderItem>()
            .Navigation(x => x.Product)
            .AutoInclude();
        builder
            .Entity<OrderItem>()
            .Navigation(x => x.Price)
            .AutoInclude();

        builder
            .Entity<Restaurant>()
            .Navigation(x => x.Location)
            .AutoInclude();
        builder
            .Entity<Restaurant>()
            .Navigation(x => x.Products)
            .AutoInclude();

        foreach (var foreignKey in builder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    public override int SaveChanges()
    {
        FixEntities(this);
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        FixEntities(this);
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging(true);
    }

    private static void FixEntities(AppDbContext context)
    {
        var dateProperties = context.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(z => new
            {
                ParentName = z.DeclaringType.Name,
                PropertyName = z.Name
            });

        var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var entity in editedEntitiesInTheDbContextGraph)
        {
            var entityFields = dateProperties
                .Where(d => d.ParentName == entity.GetType().FullName);

            foreach (var property in entityFields)
            {
                var prop = entity
                    .GetType()
                    .GetProperty(property.PropertyName);

                if (prop == null)
                    continue;

                if (prop.GetValue(entity) is not DateTime originalValue)
                    continue;

                prop.SetValue(entity, DateTime.SpecifyKind(originalValue, DateTimeKind.Utc));
            }
        }
    }
}