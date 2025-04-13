using dopeClothes.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace dopeClothes.Server.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Product>Products { get; set; }
    }
}
