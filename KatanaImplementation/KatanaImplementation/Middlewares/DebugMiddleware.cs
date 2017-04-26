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
        DebugMiddlewareOptions _options;
        public DebugMiddleware(AppFunc next, DebugMiddlewareOptions options)
        {
            _next = next;
            _options = options;
            if (_options.OnIncomingRequest == null)
                _options.OnIncomingRequest = (ctx) =>
                {
                    Debug.WriteLine("Incoming Request:" + ctx.Request.Path);
                };

            if (_options.OnOutgoingRequest == null)
                _options.OnOutgoingRequest = (ctx) =>
                  {
                      Debug.WriteLine("OutgoingRequest:" + ctx.Request.Path);
                  };
        }

        public async Task Invoke(IDictionary<string, Object> environment)
        {
            var context = new OwinContext(environment);
            // var path = environment["owin.RequestPath"] as string;

            //Debug.WriteLine("Incoming Request" + context.Request.Path);
            _options.OnIncomingRequest(context);

            await _next(environment);

            //Debug.WriteLine("Outing Request" + context.Request.Path);
            _options.OnOutgoingRequest(context);


        }
    }
}