using Microsoft.EntityFrameworkCore;

namespace EComm.API;

public class ECommDb : DbContext
{
    public ECommDb(DbContextOptions<ECommDb> options)
        : base(options) {  }

    public DbSet<Product> Products { get; set; }
}
