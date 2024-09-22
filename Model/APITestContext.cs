using Microsoft.EntityFrameworkCore;

namespace APITest.Model;

public class APITestContext : DbContext
{
    public APITestContext(DbContextOptions<APITestContext> options)
        : base(options)
    {
    }

    public DbSet<APITestItems> TodoItems { get; set; } = null!;
}