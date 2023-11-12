using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Controllers;
public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationUser _user;

    public HomeController(ILogger<HomeController> logger, ApplicationUser user) {
        _logger = logger;
        _user = user;
    }

    public IActionResult Index() => View();

    public IActionResult Privacy() => View();

    public IActionResult Register() => View();

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User user) {
        // Check if there is already such an email
        List<User> pplWithThisEmail = _user.UserRegistration.Where(u => u.Email == user.Email)?.ToList() ?? new();
        if (pplWithThisEmail.Any()) {
            _user.RemoveRange(pplWithThisEmail);
            _user.SaveChanges();
        }
        user.RegDate = DateTime.Now;
        _user.Add(user);
        _user.SaveChanges();
        ViewBag.message = $"Задание пользователя {user.Name} сохранено.";
        if (pplWithThisEmail.Any()) ViewBag.message += $" Предыдущие записи с {user.Email} были удалены.";
        return View("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
