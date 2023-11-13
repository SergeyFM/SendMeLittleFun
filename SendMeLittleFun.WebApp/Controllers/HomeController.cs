using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SendMeLittleFun.WebApp.Models;
using SendMeLittleFun.WebApp.Services;

namespace SendMeLittleFun.WebApp.Controllers;
public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _user;
    private readonly IJobManager _jobManager;
    private readonly IRandomFunEmailGenerator _randomFunEmailGenerator;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext user, IJobManager jobManager, IRandomFunEmailGenerator randomFunEmailGenerator) {
        _logger = logger;
        _user = user;
        _jobManager = jobManager;
        _randomFunEmailGenerator = randomFunEmailGenerator;
    }

    public IActionResult Index() => View();

    public IActionResult Privacy() => View();

    public IActionResult Register() => View();

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User user) {
        // Filter potential data errors
        if (user is null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Schedule) || string.IsNullOrWhiteSpace(user.Name)) {
            ViewBag.message = "Ошибка. Попробуйте ещё раз пожалуйста.";
            return View("Index");
        }

        // Check if there is already such an email
        List<User> pplWithThisEmail = _user.UserRegistration.Where(u => u.Email == user.Email)?.ToList() ?? new();
        if (pplWithThisEmail.Any()) {
            _user.RemoveRange(pplWithThisEmail);
            _user.SaveChanges();
        }
        user.RegDate = DateTime.Now;
        _user.Add(user);
        _user.SaveChanges();
        string message = $"Задание пользователя {user.Name} сохранено.";
        //if (pplWithThisEmail.Any()) message += $" Предыдущие записи с {user.Email} были удалены.";
        ViewBag.message = message;


        // Form email
        Email email = _randomFunEmailGenerator.ComposeEmail(user.Email, user.Name);

        _jobManager.AddEmailJob(email, user.Schedule);

        return View("JobAdded");
    }

    [HttpPost]
    public IActionResult DeleteByEmail(User user) {

        int count =  _jobManager.DeleteEmailJob(user.Email);
        ViewBag.message = $"Количество удалённых заданий с адресом {user.Email}: {count}";

        return View("UserDeleted");
    }

     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
