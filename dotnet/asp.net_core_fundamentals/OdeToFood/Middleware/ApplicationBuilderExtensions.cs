using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace OdeToFood.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root)
        {
            var options = new StaticFileOptions();
            options.RequestPath = "/node_modules";
            string path = Path.Combine(root, "node_modules");
            var fileProvider =  new PhysicalFileProvider(path);
            options.FileProvider = fileProvider;
            app.UseStaticFiles(options);
            return app;
        }
    }

}