using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarWorkshop.Models;

namespace CarWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult NoAccess()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        var model = new List<Person>()
        {
            new Person()
            {
                FirtsName = "Marycha",
                LastName = "Zjerycha"
            },
            new Person()
            {
                FirtsName = "Włodek",
                LastName = "Pawlik"
            }
        };
        return View(model);
    }
    public IActionResult About()
    {
        var model = new List<About>()
        {
            new About()
            {
                Title = "Tytuł nr 1",
                Description = "Opis nr 1",
                Tags = new List<string> {"koń", "kaczka", "kozioł"}

            },
            new About()
            {
                Title = "Tytuł nr 2",
                Description = "Opis nr 2",
                Tags = new List<string> {"koń2", "kaczka2", "kozioł2"}

            },
            new About()
            {
                Title = "Tytuł nr 3",
                Description = "Opis nr 3",
                Tags = new List<string> {"koń3", "kaczka3", "kozioł3"}

            }


        };
        return View(model);
    }
    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
