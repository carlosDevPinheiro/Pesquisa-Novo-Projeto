using Ambientes.WEB.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ambientes.WEB
{

    public class Startup : IStartup
    {
        private IConfiguration? Configuration { get; set; }
        private IHostEnvironment Ambiente { get; }
        public Startup(IConfiguration config, IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            #region Debugger com Info Sensiveis

            //if (hostEnvironment.IsProduction())
            //{
            //    builder.AddUserSecrets<Startup>();
            //}

            #endregion
            
            Ambiente = hostEnvironment;
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string? connString = null;



            // TODO:READ  AQUI definimos as credenciais que a aplicação vai usar no ambiente setado
           
            connString = Ambiente.IsProduction() 
                ? Environment.GetEnvironmentVariable("JAVA_HOME") 
                : Configuration.GetConnectionString("DefaultConnection");

           
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connString ?? ""));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                 .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

        }
    }

    public interface IStartup
    {
        public void ConfigureServices(IServiceCollection services);
        public void Configure(WebApplication app, IWebHostEnvironment environment);
    }


    public static class StartupExtensions
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder webAppBuilder, IHostEnvironment environment)
            where TStartup : IStartup
        {
            var startup = Activator.CreateInstance(typeof(TStartup), webAppBuilder.Configuration,environment) as IStartup;

            if (startup == null) throw new ArgumentException("Classe Startup.cs invalida");

            startup.ConfigureServices(webAppBuilder.Services);

            var app = webAppBuilder.Build();
           
            startup.Configure(app, app.Environment);

            app.Run();
            return webAppBuilder;

        }
    }
}
