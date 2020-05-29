using InsideAirbnbApp.Models;
using InsideAirbnbApp.Repositories;
using InsideAirbnbApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InsideAirbnbApp
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
            // Performance
            services.AddResponseCompression();

            // Profiling tools
            services.AddMiniProfiler(options =>
                {
                    options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
                    options.PopupShowTimeWithChildren = true;
                }).AddEntityFramework();

            // Database and Redis cache server
            services.AddDbContext<AirbnbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AirBnb")));
            services.AddStackExchangeRedisCache(options => {
                options.Configuration = Configuration.GetConnectionString("Redis");
                options.InstanceName = "InsideAirbnbRedis";
            });

            // Repository pattern
            services.AddScoped<IRepository<NeighbourhoodsViewModel>, NeighbourhoodsRepository>();
            services.AddScoped<IRepository<ListingsViewModel>, ListingsRepository>();

            // Azure Active Directory B2C
            services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme).AddAzureADB2C(options => Configuration.Bind("AzureAdB2C", options));
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Reader", policy => policy.RequireClaim("extension_Role", "Reader"));
                options.AddPolicy("Admin", policy => policy.RequireClaim("extension_Role", "Admin"));
            });

            // Default 
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Increases performance by compression
            app.UseResponseCompression();

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

            //Block HTTP headers for security
            app.Use(async (context, next) =>
            {
                // Blocks web page from being included in <frame>, <iframe>, <embed> or <object>
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                // How much referrer info must included in requests
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                // Blocks a request if the request destination is with a wrong type and MIME type combo 
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                // Cross side scripting protection
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

                await next();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMiniProfiler();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("api", "api/{controller}/{action}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
