using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;

namespace WebApi
{
    public class SqlDbContext : DbContext
    {
        protected SqlDbContext()
        {
        }
        public SqlDbContext(DbContextOptions options) : base(options)
        {
        }


        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
    }
}
