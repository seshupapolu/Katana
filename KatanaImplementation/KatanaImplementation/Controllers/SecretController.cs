using System.Web.Mvc;

namespace KatanaImplementation.Controllers
{
    [Authorize]
    public class SecretController : Controller
    {
        // GET: Secret
        public ActionResult Index()
        {
            return View();
        }
    }
}