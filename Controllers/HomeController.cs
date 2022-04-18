using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using assignment_log_and_reg.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment_log_and_reg.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
      _logger = logger;
      _context = context;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost("users/add")]
    public IActionResult AddUser(User newUser)
    {
      Console.WriteLine("ADDING FUNCTION");
      if (ModelState.IsValid)
      {
        _context.Add(newUser);
        _context.SaveChanges();

        return RedirectToAction("Success");
      }
      else
      {
        return View("Index");
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
  }
}
