using AutoMapper;
using Involys.Poc.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Involys.Poc.Api;
using Involys.Poc.Api.Utils.Logger;
using Involys.Poc.Api.Utils.HealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Involys.Poc.Api.Services.Tables;
using Involys.Poc.Api.Controllers.Table.Model;
using System.Reflection;

using Involys.Poc.Api.Services.DataSources;
using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.Poc.Api.Services.ExpressionTerms;
using Involys.Poc.Api.Services.DataSourceFields;
using Involys.Poc.Api.Services.DataSourceTables;
using Involys.Poc.Api.services.DynamicLinq;

namespace PocExtApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // initialize configuration
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddControllers();


            if ("SqlServer".Equals(appSettings.Database))
            {
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),b=> b.MigrationsAssembly("Web")));
                System.Console.WriteLine("connected");
            }
            else if ("InMemory".Equals(appSettings.Database))
            {
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseInMemoryDatabase("IntegrationTestsDatabase"));
            }
            else if ("Oracle".Equals(appSettings.Database))
            {
        
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseOracle(Configuration.GetConnectionString("OracleConnection"),
                        options => options.UseOracleSQLCompatibility("11"))
                    );
            }
            else if ("Postgres".Equals(appSettings.Database))
            {
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("PostgresConnection")
                        
                    ));
            }
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            //services.AddAutoMapper(Assembly.GetAssembly(typeof(DataSourceResponse))); //GetType().Assembly) ;


            services.AddAutoMapper(GetType().Assembly);

            // configure maximum multipart from body size
            services.Configure<FormOptions>(options
                => options.MultipartBodyLengthLimit = appSettings.MaximumFormBodyLength);

            AddCustomHealthCheck(services, Configuration, appSettings);



            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            

          
            MapInterfacesWithImplementations(services);
            services.AddCors();

            //https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-3.1&tabs=visual-studio
            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Dynamic Query Builder";
                    document.Info.Description = "Dynamic Query Builder";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Dynamic Query Builder",
                        Email = string.Empty,
                        Url = "https://google.com"
                    };                   
                };
            });
            ExtendConfigureServices(services);

        }

        public void AddCustomHealthCheck(IServiceCollection services, IConfiguration Configuration, AppSettings appSettings)
        {
            if ("SqlServer".Equals(appSettings.Database))
            {
                services.AddHealthChecks().AddCheck(
                                "DB-check",
                                new SqlServerConnectionHealthCheck(Configuration.GetConnectionString("DefaultConnection")),
                                HealthStatus.Unhealthy,
                                new string[] { "db" });

            }
            else
            {
                services.AddHealthChecks();
            }
        }

        public virtual void ExtendConfigureServices(IServiceCollection services)
        {

        }
        public static void MapInterfacesWithImplementations(IServiceCollection services)
        {
            // configure DI for application services
            
            services.AddScoped<ITableService, TableService>();
            
            services.AddScoped<IDataSourceService, DataSourceService>();
            services.AddScoped<IExpressionTermService, ExpressionTermService>();
            services.AddScoped<IDataSourceFieldService, DataSourceFieldService>();
            services.AddScoped<IDataSourceTableService, DataSourceTableService>();
            services.AddScoped<IDynamicLinqService, DynamicLinqService>();
            


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder => builder.WithOrigins("http://localhost:5200").AllowAnyHeader().AllowAnyMethod());

            }
            else
            {
                app.UseExceptionHandler("/error");
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapHealthChecks("api/v1/health");
            });

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            ExtendConfigure(app, env);
        }

        public virtual void ExtendConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
