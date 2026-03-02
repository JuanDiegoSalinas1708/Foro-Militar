using System.Linq;
using System.Web.Mvc;
using Foro.Entities.Models; // si lo necesitas
// Ajusta el namespace si es diferente

namespace Foro.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        public ActionResult Index()
        {
            // Si quieres comportamiento profesional:
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");

            var communities = _context.Communities.ToList();

            return View(communities);
        }
    }
}