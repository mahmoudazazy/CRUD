using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Luftborn.Infrastructure.Data;

public class LuftbornContextFactory : IDesignTimeDbContextFactory<LuftbornContext>
{
    public LuftbornContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LuftbornContext>();
        optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=LuftbornDB;User ID=sa;Password=fil3B0und;");
        return new LuftbornContext(optionsBuilder.Options);
    }
}