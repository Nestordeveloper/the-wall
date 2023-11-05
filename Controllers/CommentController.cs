using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using the_wall.Models;

namespace the_wall.Controllers;

public class CommentController : Controller
{
    private readonly ILogger<CommentController> _logger;
    private MyContext _context;
    public CommentController(ILogger<CommentController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


}