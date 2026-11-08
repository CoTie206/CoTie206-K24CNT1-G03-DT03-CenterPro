using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace CenterPro.Data.Infrastructure;
public sealed class CenterProDbContextFactory : IDesignTimeDbContextFactory<CenterProDbContext> {
 public CenterProDbContext CreateDbContext(string[] args) { var options=new DbContextOptionsBuilder<CenterProDbContext>().UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CenterProDb;Trusted_Connection=True;TrustServerCertificate=True").Options; return new CenterProDbContext(options); }
}
