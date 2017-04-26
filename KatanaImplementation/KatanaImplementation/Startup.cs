using KatanaImplementation.Middlewares;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;

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

            app.Use(async (ctx, next) =>
            {
                await ctx.Response.WriteAsync("<html><head></head><body>hello seshu</body><html>");
            });

        }
    }
}