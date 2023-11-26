using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> User { get; set; }

    public DbSet<Studia> Studia { get; set; }

    public DbSet<Assortment> Assortments { get; set; }

    // Штука, где мы явно указываем, куда сохранять миграции (Если правильно понял)
    //TODO connectionString как-то нормально сделать
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Server=localhost; Port=5433; Database=MarketDatabase; Userid=postgres;Password=faqopl11",
            b => b.MigrationsAssembly("Market"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
        
        modelBuilder.Entity<Assortment>()
            .HasOne(s => s.Studia)
            .WithMany(a => a.Assortments)
            .HasForeignKey(a => a.AssortmentId);
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