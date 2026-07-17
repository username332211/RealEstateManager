using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Agencija.Web.Models;

namespace Agencija.Web.Controllers
{
    public class HomeController(
        ILogger<HomeController> _logger) 
        : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

		[Route("cesto-postavljana-pitanja")]
		public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }


    }
}