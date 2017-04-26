using KatanaImplementation.Middlewares;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

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
            app.Use<DebugMiddleware>(new DebugMiddlewareOptions()
            {
                OnIncomingRequest = (ctx) =>
                  {
                      var watch = new Stopwatch();
                      watch.Start();
                      ctx.Environment["TimeTook"] = watch;
                  },
                OnOutgoingRequest=(ctx)=>
                {
                    var watch = ctx.Environment["TimeTook"] as Stopwatch;
                    watch.Stop();
                    Debug.WriteLine("TimeTook: " + watch.ElapsedMilliseconds + " ms");
                    
                }


            });
            app.Use(async (ctx, next) =>
            {
                await ctx.Response.WriteAsync("<html><head></head><body>hello seshu</body><html>");
            });

        }
    }
}