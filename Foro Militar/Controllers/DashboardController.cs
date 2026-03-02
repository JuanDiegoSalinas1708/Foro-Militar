using System.Web.Mvc;

namespace Foro.Web.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Auth");

            return View();
        }
    }
}