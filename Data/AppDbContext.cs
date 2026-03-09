using Microsoft.EntityFrameworkCore;
using StorageManagement.Models;

namespace StorageManagement.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Password> Passwords { get; set; }
    public DbSet<User> Users { get; set; }
}