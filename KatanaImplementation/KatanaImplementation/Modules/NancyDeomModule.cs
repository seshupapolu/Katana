using Nancy;
using Nancy.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KatanaImplementation.Modules
{
    public class NancyDeomModule:NancyModule
    {

        public NancyDeomModule()
        {
            Get["/nancy"] = x =>
            {
                var env = Context.GetOwinEnvironment();
                return "Hello from nancy! You Requested: " + env["owin.RequestPath"];
            };
        }
    }

}