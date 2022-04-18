using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using assignment_log_and_reg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

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
    public IActionResult Success()
    {
      return View();
    }

    [HttpGet("userlogin")]
    public IActionResult UserLogin(){

      return View();
    }

    [HttpPost("users/add")]
    public IActionResult AddUser(User newUser)
    {
      Console.WriteLine("ADDING FUNCTION");
      if (ModelState.IsValid)
      {

        // Duplicate Emails
        if (_context.Users.Any(s => s.Email == newUser.Email))
        {
          ModelState.AddModelError("Email", "This email already in use.");
          return View("Index");
        }

        // Hash Pass
        PasswordHasher<User> Hasher = new PasswordHasher<User>();
        newUser.Password = Hasher.HashPassword(newUser, newUser.Password);


        _context.Add(newUser);
        _context.SaveChanges();

        // Login(newUser);

        return RedirectToAction("Success");
      }
      else
      {
        return View("Index");
      }
    }


    [HttpPost("users/login")]
    public IActionResult Login(LoginUser loginUser)
    {
      Console.WriteLine("IN LOGIN FUNCTION");
      if (ModelState.IsValid)
      {
        // Find User
        User userInDb = _context.Users.FirstOrDefault(d => d.Email == loginUser.Email);
        if (userInDb == null)
        {
          ModelState.AddModelError("Email", "Invalid Email/Password");
          return View("Index");
        }
        // Check Password
        PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
        var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.Password);
        if (result == 0)
        {
          ModelState.AddModelError("Email", "Invalid Email/Password");
          return View("Index");
        }


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
