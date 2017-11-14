﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin.Demo.Middleware;

namespace Owin
{
    public static class DebugMiddlewareExtension
    {
        public static void UseDebugMIddlewareExtensions(
            this IAppBuilder app,
            DebugMiddlewareOptions options = null)
        {
            if (options == null)
            {
                options = new DebugMiddlewareOptions();
            }

            app.Use<DebugMiddleware>(options);
        }
    }
}