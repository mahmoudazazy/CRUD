using Law.API;
using Serilog.Context;
using Serilog;
using Luftborn.Infrastructure.Data;
using Luftborn.API.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
            CreateHostBuilder(args)
            .Build()
                .MigrateDatabase<LuftbornContext>((context, services) =>
                {
                })
                .Run();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).UseSerilog();
}