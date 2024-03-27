using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Luftborn.Core.Repositories;
using Luftborn.Infrastructure.Data;
using Luftborn.Infrastructure.Repositories;

namespace Luftborn.Infrastructure.Extensions;

public static class InfraServices
{
    public static IServiceCollection AddInfraServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<LuftbornContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("LuftbornConnectionString"))); 

        serviceCollection.AddScoped(typeof(LuftbornContext), typeof(LuftbornContext));
        serviceCollection.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        return serviceCollection;
    }
}