namespace TestAPI.Model
{
    using Microsoft.EntityFrameworkCore;
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }

        public DbSet<RegisterdUser> Users { get; set; }
    }
}
