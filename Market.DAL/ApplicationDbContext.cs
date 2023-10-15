using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    // Штука, где мы явно указываем, куда сохранять миграции (Если правильно понял)
    //TODO connectionString как-то нормально сделать
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-GRH4NDM;Database=MarketDatabase;Trusted_Connection=True",
            b => b.MigrationsAssembly("Market"));
    }

    public DbSet<Car> Car { get; set; }
    
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
        });
    }
}
