using Attendance.Infrastructure.Data.Common;
using Attendance.Infrastructure.loc;
using Microsoft.AspNetCore.Mvc;

namespace Attendance
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);

            });

            services.AddMvc();
            services.AddControllersWithViews();
            RegisterServices(services);
            //services.AddDbContext<ELoanSystemContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ELoanSystemDB")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(Directory.GetCurrentDirectory(), @"Content")),
            //    RequestPath = new PathString("/Content")

            //});

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();
            //app.UseMiddleware<SessionExpirationMiddleware>();

            //app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            Container.RegisterServices(services);
            //services.AddSingleton<JwtAuthManager>();
        }
    }
}
