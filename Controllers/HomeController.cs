using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using the_wall.Models;

namespace the_wall.Controllers;

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

    // REGISTRO
    [HttpGet]
    [Route("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Route("users/register")]
    public IActionResult ProcessRegister(User newUser)
    {
        try
        {
            if (ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                _context.Add(newUser);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "¡Registro exitoso! Ingrese sus credenciales en el Log in para iniciar sesión.";

                return RedirectToAction("Messages", "Message");
            }
            else
            {
                return View("Index");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al registrar el usuario: " + ex.Message);
            return View("Index");
        }
    }



    //LOGIN
    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        return View("Index");
    }

    [HttpPost]
    [Route("users/login")]
    public IActionResult ProcessLogin(Login userSubmission)
    {
        if (ModelState.IsValid)
        {
            User? userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.EmailLog);

            if (userInDb == null)
            {
                ModelState.AddModelError("EmailLog", "This user does not exist");
                ModelState.AddModelError("PasswordLog", "This user does not exist");
                Console.WriteLine("Usuario no registrado error");
                return View("Index");
            }

            PasswordHasher<Login> hasher = new PasswordHasher<Login>();
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.PasswordLog);

            if (result == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetString("Email", userInDb.Email);
                HttpContext.Session.SetString("FirstName", userInDb.FirstName);
                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                return RedirectToAction("Messages", "Message");
            }
            else
            {
                ModelState.AddModelError("EmailLog", "Invalid Email/Password");
                ModelState.AddModelError("PasswordLog", "Invalid Email/Password");
                Console.WriteLine("Credenciales invalidas");
                return View("Index");
            }
        }
        else
        {
            return View("Index");
        }
    }

    //LOGOUT
    [HttpPost]
    [Route("logout")]
    public IActionResult ProcessLogout(string logout)
    {
        if (logout == "logout")
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        return RedirectToAction("Messages", "Message");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string? email = context.HttpContext.Session.GetString("Email");

        if (email == null)
        {
            context.Result = new RedirectToActionResult("Login", "Home", null);
        }
    }
}