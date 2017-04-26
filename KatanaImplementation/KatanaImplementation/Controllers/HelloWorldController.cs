using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace KatanaImplementation.Controllers
{
    [RoutePrefix("api")]
    public class HelloWorldController : ApiController
    {
        [Route("hello")]
        [HttpGet]
        public IHttpActionResult Helloworld()
        {
            return Content(HttpStatusCode.OK, "hello world");
        }

    }
}