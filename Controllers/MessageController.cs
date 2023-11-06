using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using the_wall.Models;

namespace the_wall.Controllers;

public class MessageController : Controller
{
    private readonly ILogger<MessageController> _logger;
    private MyContext _context;
    public MessageController(ILogger<MessageController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    // MESSAGE GET VIEW
    [HttpGet]
    [Route("messages")]
    public IActionResult Messages()
    {
        return View("Messages");
    }
}