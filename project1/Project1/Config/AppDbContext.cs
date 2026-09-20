using Microsoft.EntityFrameworkCore;
namespace Project1.Config
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
