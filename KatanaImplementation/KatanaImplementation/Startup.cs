﻿using KatanaImplementation.Middlewares;
using Owin;
using System.Diagnostics;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

namespace KatanaImplementation
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            //app.Use(async (ctx, next) =>
            //{
            //    Debug.WriteLine("Incoming Request" + ctx.Request.Path);
            //    await next();
            //    Debug.WriteLine("Outgoing Request" + ctx.Request.Path);
            //});

            //Below is the alternate for above

            app.UseDebugMidddleware();
            // or
            app.UseDebugMidddleware(new DebugMiddlewareOptions()
            {
                OnIncomingRequest = (ctx) =>
                  {
                      var watch = new Stopwatch();
                      watch.Start();
                      ctx.Environment["TimeTook"] = watch;
                  },
                OnOutgoingRequest = (ctx) =>
                  {
                      var watch = ctx.Environment["TimeTook"] as Stopwatch;
                      watch.Stop();
                      Debug.WriteLine("TimeTook: " + watch.ElapsedMilliseconds + " ms");

                  }
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Auth/Login")
            });

            app.Use(async (ctx, next) =>
            {
                if (ctx.Authentication.User.Identity.IsAuthenticated)
                    Debug.WriteLine("User: " + ctx.Authentication.User.Identity.Name);
                else
                    Debug.WriteLine("user not Authenticated");
                await next();
            });

            //Below is the alternate for above
            //app.Use<DebugMiddleware>(new DebugMiddlewareOptions()
            //{
            //    OnIncomingRequest = (ctx) =>
            //      {
            //          var watch = new Stopwatch();
            //          watch.Start();
            //          ctx.Environment["TimeTook"] = watch;
            //      },
            //    OnOutgoingRequest = (ctx) =>
            //      {
            //          var watch = ctx.Environment["TimeTook"] as Stopwatch;
            //          watch.Stop();
            //          Debug.WriteLine("TimeTook: " + watch.ElapsedMilliseconds + " ms");

            //      }


            //});


            //This statement calls the UseNancy other than /nancy url
            //app.Map("/nancy", mappedApp => { mappedApp.UseNancy(); });
            //To Use nancy install-package nancy.owin
            //app.UseNancy();

            //WebApi registration install-package microsoft.aspnet.webapi.owin
            var configuration = new HttpConfiguration();
            configuration.MapHttpAttributeRoutes();

            app.UseWebApi(configuration);



            //Below code is not required if we passing request to MVC
            //If any pipepline is prepared with response, then request is not passed to MVC.
            //app.Use(async (ctx, next) =>
            //{
            //    await ctx.Response.WriteAsync("<html><head></head><body>hello seshu</body><html>");
            //});

        }
    }
}