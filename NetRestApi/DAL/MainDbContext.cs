using Microsoft.EntityFrameworkCore;
using NetRestApi.Entities;

namespace NetRestApi.DAL;

public class MainDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public MainDbContext(DbContextOptions options, IConfiguration configuration): base(options)
    {
        _configuration = configuration;
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CartProductsMap());
        base.OnModelCreating(modelBuilder);
    }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("WebApiDatabase"));
    }
    
    //table name
    public DbSet<Product?> Products { get; set; }
    public DbSet<Cart> Cart { get; set; }
    public DbSet<CartProducts> CartProducts { get; set; }
}