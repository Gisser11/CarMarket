using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Market.DAL;

public class ApplicationDbContext : DbContext
{
    #region BackendAndMigrationsConfig

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("MarketDatabase");

            optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("Market"));
        }
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    #endregion

    public DbSet<User> User { get; set; }

    public DbSet<Studia> Studia { get; set; }

    public DbSet<Assortment> Assortments { get; set; }

    public DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
        
        modelBuilder.Entity<Assortment>()
            .HasOne(s => s.Studia)
            .WithMany(a => a.Assortments)
            .HasForeignKey(a => a.AssortmentId);

        modelBuilder.Entity<Service>().HasOne(s => s.Studia)
            .WithMany(_services => _services.Services)
            .HasForeignKey(_services => _services.ServicesId);
    }
}

/*
INSERT INTO "Studia" ("Name", "City", "DataCreate", "MedianPrice", "Rating", "TypeStudia", "TypeAdvantages")
VALUES ('Название 1', 'Город 1', '2023-11-25', 100.00, 4.5, 1, 0),
       ('Название 2', 'Город 2', '2023-11-26', 150.00, 4.7, 2, 1);

-- Заполнение таблицы assortment, связанной с studia
INSERT INTO "Assortments" ("Id", "Name", "Price", "AssortmentId")
VALUES (1, 'Товар 1', '50.00', 1),
       (2, 'Товар 2', '75.00', 1),
       (3, 'Товар 3', '80.00', 2);
 */