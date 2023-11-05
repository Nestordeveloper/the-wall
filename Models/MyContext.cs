#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace the_wall.Models;
public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
}
