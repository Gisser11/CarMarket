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

    public DbSet<Car> Car { get; set; }
}