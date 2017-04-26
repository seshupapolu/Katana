using KatanaImplementation.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Owin
{
    public static class AppBuilderExtensions
    {
        public static void UseDebugMidddleware(this IAppBuilder app, DebugMiddlewareOptions options = null)
        {
            if (options == null)
                options = new DebugMiddlewareOptions();

            app.Use<DebugMiddleware>(options);
        }

    }
}