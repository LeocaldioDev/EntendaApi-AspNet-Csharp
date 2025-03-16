using Microsoft.EntityFrameworkCore;
using PrimeiraApi.Domain.Model.CompanyAggregate;
using PrimeiraApi.Domain.Model.UserAggregate;

namespace PrimeiraApi.Infrastructura
{
    public class DbConnection : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company>company { get; set; }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=localhost;" +
                        "Port=5432;Database=PrimeiraApi;" +
                        "User Id=postgres;" +
                        "Password=536539;");

    }
}
