using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartnerDiaries.Models;

namespace PartnerDiaries.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("isLogged") == "true")
        {
            return View();
        }
        return RedirectToAction("Login");
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult LoginPost()
    {
        if (Request.Form["password"].ToString().ToLower() == "admin" && Request.Form["username"].ToString().ToLower() == "admin")
        {
            HttpContext.Session.SetString("isLogged", "true");
            return RedirectToAction("Index");
        }
        else
        {
            return RedirectToAction("Login");
        }
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public string Protocols()
    {
        return "hello world!";
    }
}
