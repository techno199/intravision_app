using Microsoft.EntityFrameworkCore;

namespace server.Models
{
  public class Context : DbContext
  {
    public Context(DbContextOptions<Context> options)
      : base(options)
      {

      }

      public DbSet<Device> Devices { get; set; }
      public DbSet<Coin> Coins { get; set; }
      public DbSet<Drink> Drinks { get; set; }
  }
}