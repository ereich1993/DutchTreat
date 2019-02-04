using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DutchTreat.Models;
using DutchTreat.Services;
using DutchTreat.Data;
using Microsoft.AspNetCore.Authorization;

namespace DutchTreat.Controllers
{
    public class HomeController : Controller
    {
        private IMailService _mailService;
        private IDutchRepository _dutchContext;
        private DutchSeeder _seeder;

        public HomeController(IMailService mailService, IDutchRepository dutchContext, DutchSeeder seeder)
        {
            _mailService = mailService;
           _dutchContext = dutchContext;
            _seeder = seeder;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Title = "Contact.Us";
            return View();
        }

        [HttpPost]
        public IActionResult Contact(User UserForm)
        {
            if (ModelState.IsValid)
            {
                _mailService.send(UserForm);
                
            }
            return View();

        }

        public IActionResult Privacy()
        {
            _seeder.SeedAsync().Wait();
            return View();
        }


        [Authorize]
        public IActionResult Shop()
        {
            var results = _dutchContext.GetAllProducts();
            return View(results.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
