using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MSIT158Site.Models;

namespace MSIT158Site.Controllers
{
    public class HomeworkController : Controller
    {

        private readonly MyDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeworkController(MyDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Homework1()
        {
            return View();
        }

        public IActionResult homework2()
        {
            return View();
        }
    }
}
