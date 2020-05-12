using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AcmeCorporation.Models;
using ageCalc;

namespace AcmeCorporation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAgeCalculation _verifier;

        public HomeController(ILogger<HomeController> logger, IAgeCalculation verifier)
        {
            _logger = logger;
            _verifier = verifier;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult VerificationError()
        {
            return View();
        }

        [Route("Home/ErrorPage/{contentId?}")]
        public IActionResult ErrorPage(int contentId)
        {
            return View(contentId);
        }

        public IActionResult AgeVerification(string dateofBirth)
        {
            DateTime dob;
            bool canParse = DateTime.TryParse(dateofBirth, out dob);
            if (canParse.Equals(true))
            {
                bool adultVerification = _verifier.Is18(dob);
                if (adultVerification.Equals(true))
                {
                    return RedirectToAction("Create", "Submissions");
                }
                else
                {
                    
                    return RedirectToAction("ErrorPage", new { contentId = 1});
                }
            }
            else
            {
                return RedirectToAction("ErrorPage", new { contentId = 2});
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
