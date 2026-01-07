namespace NetCloak.Persistence.Contexts;

using EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using NetCloak.Domain.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Adding configuration rules to the <seealso cref="ApplicationDbContext"/>.
    /// </summary>
    /// <param name="modelBuilder"><seealso cref="ModelBuilder"/> object for extending current <seealso cref="ApplicationDbContext"/> object.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
    }

    public async Task SeedOrdersAsync()
    {
        if (await Orders.AnyAsync())
        {
            return; // Skip seeding if data already exists
        }

        var orders = new List<Order>();
        for (int i = 1; i <= 1000000; i++)
        {
            orders.Add(new Order
            {
                Id = Guid.NewGuid(),
                Name = $"Order {i}",
                // Set other properties if needed
            });

            // Insert in batches to avoid performance issues
            if (i % 10000 == 0)
            {
                await Orders.AddRangeAsync(orders);
                await SaveChangesAsync();
                orders.Clear();
            }
        }

        // Insert any remaining orders
        if (orders.Any())
        {
            await Orders.AddRangeAsync(orders);
            await SaveChangesAsync();
        }
    }

    public DbSet<Order> Orders { get; set; }
}