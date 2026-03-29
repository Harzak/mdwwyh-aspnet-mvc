using System.Web.Mvc;

namespace Clean.Controllers
{
    [Authorize]
    public sealed class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
