using Luftborn.Infrastructure.Data;
using Luftborn.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Serilog;
using System.Text;
using Luftborn.Application.Extensions;
using Luftborn.Application.Services;
using Luftborn.Application.Behaviour;
using Luftborn.Application.Models;
using Microsoft.AspNetCore.Http.Features;
using Luftborn.Core.Common;
using Luftborn.Application.Services;

namespace Law.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddHttpContextAccessor();
            ConfigurationSettings(services);
            services.AddApplicationServices();
            services.AddInfraServices(_configuration);

            services.AddHttpClient();

            ConfigureScopedService(services);
            services.AddControllers()
                 .AddNewtonsoftJson(options =>
                      options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                 )
                  .AddNewtonsoftJson(options =>
                      options.SerializerSettings.Converters.Add(new StringEnumConverter())
                 );
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder => builder.AllowAnyOrigin()
                                       .AllowAnyMethod()
                                       .AllowAnyHeader()
                    );
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Your API", Version = "v1" });
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue;
            });

        }

        private void ConfigurationSettings(IServiceCollection srv)
        {
            srv.Configure<AppSettings>(_configuration.GetSection("AppSettings"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddFile(configuration.GetSection("Logging"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Luftborn API V1");
            //    c.RoutePrefix = string.Empty;
            //});
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Luftborn API V1");
            });

            app.UseHttpsRedirection();
            app.UseForwardedHeaders();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseCors(x => x
                     .AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowAnyOrigin().WithExposedHeaders("content-disposition")
            );

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllers();
            });
        }

        private void ConfigureScopedService(IServiceCollection services)
        {
            services.AddScoped<IEmployeeQuery, EmployeeQuery>(); 
        } 
    }
}