using Infra.EFCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Infra.EFCore.Helpers;
public class DbContextHelpers {

    public static readonly string _path = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ChatAppDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    // public static IConfiguration Configuration => new ConfigurationBuilder().Build();
    public static DbContextOptions<AppDbContext> DbContextOptions {
        get {
            var dbContextOptionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptionBuilder.UseSqlServer(_path);
            return dbContextOptionBuilder.Options;
        }
    }
    public static AppDbContext CreateDbContext() => new(DbContextOptions);

    public static Mock<AppDbContext> CreateMockDbContext() => new(DbContextOptions);

}
