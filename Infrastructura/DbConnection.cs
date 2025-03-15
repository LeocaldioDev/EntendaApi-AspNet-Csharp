using Microsoft.EntityFrameworkCore;
using PrimeiraApi.Domain.Model;

namespace PrimeiraApi.Infrastructura
{
    public class DbConnection : DbContext
    {
        public DbSet<User> Users { get; set; }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=localhost;" +
                        "Port=5432;Database=PrimeiraApi;" +
                        "User Id=postgres;" +
                        "Password=536539;");

    }
}
