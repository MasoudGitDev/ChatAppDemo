using Infra.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infra.EFCore.Contexts;  
internal class AppDbContextDesignFactory : IDesignTimeDbContextFactory<AppDbContext> {

    public AppDbContext CreateDbContext(string[] args) {
        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var path = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ChatAppDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        optionBuilder.UseSqlServer(path);
        return new AppDbContext(optionBuilder.Options);
    }
}
