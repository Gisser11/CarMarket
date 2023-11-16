using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Car { get; set; }

    public DbSet<User> User { get; set; }

    public DbSet<Studia> Studia { get; set; }

    public DbSet<Assortment> Assortments { get; set; }

    // Штука, где мы явно указываем, куда сохранять миграции (Если правильно понял)
    //TODO connectionString как-то нормально сделать
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-GRH4NDM;Database=FullStackMarket;Trusted_Connection=True",
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