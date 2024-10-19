using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> context) : base(context)
    {


    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
}
