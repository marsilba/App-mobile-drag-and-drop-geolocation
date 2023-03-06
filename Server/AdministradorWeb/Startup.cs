using AdministradorWeb.Repositorio;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdministradorWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MySqlContext>(
                options => options.UseMySql(Configuration.GetConnectionString("MySqlConnect"))
            );

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
                });

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAnyOrigin",
            //    builder => builder.AllowAnyOrigin());
            //});

            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            //else
            //    app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();
            app.UseCookiePolicy();


            //app.UseCors("AllowAnyOrigin");

            app.UseCors(option => option.AllowAnyOrigin()); ;

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=LoginFinanceiro}/{id?}");
            });
        }
    }
}
