using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WiredBrainCoffee.Services;

namespace WiredBrainCoffee
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
            services.AddRazorPages().AddRazorPagesOptions(x =>
            {
                x.Conventions.AddPageRoute("/index", "home");
                x.Conventions.AddPageRoute("/index", "wired");
            });

            services.Configure<RouteOptions>(x =>
            {
                x.ConstraintMap.Add("promo", typeof(PromoConstraint));
            });

            services.AddScoped<IMenuService, MenuService>();
            services.AddLogging();  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                if(context.Request.Path.Value.Contains("alive"))
                {
                    await context.Response.WriteAsync("The app is alive");
                }
                else
                {
                    Console.WriteLine("Before Razor Pages");
                    await next.Invoke();
                    Console.WriteLine("After Razor Pages");
                }
            });

            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
