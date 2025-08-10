using System.Diagnostics;
using System.Linq;
using CafeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CafeApp.Data;

namespace CafeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = _context.Users.FirstOrDefault(u => u.Username == User.Identity.Name);

                if (currentUser != null &&
                    currentUser.DateOfBirth.Month == DateTime.Now.Month &&
                    currentUser.DateOfBirth.Day == DateTime.Now.Day)
                {
                    TempData["BirthdayMessage"] = " Happy Birthday! You get 20% off all items today!";
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
