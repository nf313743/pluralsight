using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Owin.Demo.Middleware;
using Nancy.Owin;
using Nancy;
using System.Web.Http;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;

namespace Owin.Demo
{
    public static class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            app.UseDebugMIddlewareExtensions(new DebugMiddlewareOptions
            {
                OnIncomingRequest = (ctx) =>
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    ctx.Environment["DebugStopWatch"] = watch;
                },
                OnOutgoingRequest = (ctx) =>
                {
                    var watch = (Stopwatch)ctx.Environment["DebugStopWatch"];
                    watch.Stop();
                    Debug.WriteLine($"Request took: {watch.ElapsedMilliseconds} ms");
                }
            });

            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {
                    AuthenticationType = "ApplicationCookie",
                    LoginPath = new PathString("/Auth/Login")
                });

            app.Use(async (ctx, next) =>
            {
                if (ctx.Authentication.User.Identity.IsAuthenticated)
                {
                    Debug.WriteLine("User " + ctx.Authentication.User.Identity.Name);
                }
                else
                {
                    Debug.WriteLine("User not authenticated");
                }
                await next();
            });

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);

            //app.UseNancy(conf =>
            //{
            //    conf.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            //});

            app.Map("/nancy", mappedApp => { mappedApp.UseNancy(); });

            //app.Use(async (ctx, next) =>
            //{
            //    await ctx.Response.WriteAsync("<html><head></head><body>Hello World</body></html>");
            //});
        }
    }
}