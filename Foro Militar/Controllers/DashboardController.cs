using Foro.Web.Services;
using System.Web.Mvc;

namespace Foro.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;

        public DashboardController()
        {
            _dashboardService = new DashboardService();
        }

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Auth");

            var model = _dashboardService.BuildHome();
            return View(model);
        }
    }
}