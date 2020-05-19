using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TP_asp_Yicheng_Line.Models;
using TP_asp_Yicheng_Line.DB;
using TP_asp_Yicheng_Line.DB.Models;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace TP_asp_Yicheng_Line.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string connectionString;

        public HomeController(ILogger<HomeController> logger, IConfiguration configRoot)
        {
            _logger = logger;
            connectionString = configRoot["ConnectionString:DefaultConnection"];
        }

        public IActionResult Index()
        {
            CategoryContext questCategories = new CategoryContext(connectionString);
            List<Category> categories = questCategories.GetAll();
            AcceuilViewModel model = new AcceuilViewModel();
            model.Categories = categories;
            return View(model);
            
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
