using Foro.Entities.Models;
using System.Linq;
using System.Web.Mvc;

public class AuthController : Controller
{
    private readonly AppDbContext _context;

    public AuthController()
    {
        _context = new AppDbContext();
    }

    // =========================================
    // GET: /Auth/Register
    // =========================================
    [HttpGet]
    public ActionResult Register()
    {
        return View();
    }

    // =========================================
    // POST: /Auth/Register
    // =========================================
    [HttpPost]
    public ActionResult Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        if (_context.Users.Any(u => u.Email == request.Email))
        {
            ModelState.AddModelError("", "El email ya existe");
            return View(request);
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }

    // =========================================
    // GET: /Auth/Login
    // =========================================
    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }

    // =========================================
    // POST: /Auth/Login
    // =========================================
    [HttpPost]
    public ActionResult Login(LoginRequest request)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Email == request.Email);

        if (user == null ||
            !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            ModelState.AddModelError("", "Credenciales inválidas");
            return View(request);
        }

        // Guardamos sesión
        Session["UserId"] = user.Id;
        Session["Username"] = user.Username;
        Session["Role"] = user.Role;

        return RedirectToAction("Index", "Home");
    }

    // =========================================
    // LOGOUT
    // =========================================
    public ActionResult Logout()
    {
        Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}