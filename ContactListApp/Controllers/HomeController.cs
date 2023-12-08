using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactListApp.Models;
using ContactListApp.Data;

namespace ContactListApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _dbContext;


    public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;

    }

    public IActionResult Index()
    {
        var contacts = _dbContext.Contacts.ToList();
        return View(contacts);
    }

    public IActionResult Details(int id)
    {
        var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == id);

        if (contact == null)
        {
            return NotFound();
        }

        return View(contact);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
