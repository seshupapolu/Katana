using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace KatanaImplementation.Middlewares
{
    public class DebugMiddleware
    {
        AppFunc _next;
        public DebugMiddleware(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, Object> environment)
        {
            var context = new OwinContext(environment);
            // var path = environment["owin.RequestPath"] as string;
            Debug.WriteLine("Incoming Request" + context.Request.Path);
            await _next(environment);
            Debug.WriteLine("Outing Request" + context.Request.Path);


        }
    }
}