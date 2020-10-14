using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SumOfAllFearsCore.Models;

namespace SumOfAllFearsCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Code()
        {
            return View();
        }
        public IActionResult Solve()
        {
            ViewData["Found"] = false;
            return View();
        }

        [HttpPost]
        public IActionResult Solve(int num1, int num2, int num3, int num4, int num5, int kValue)
        {
            int[] inputArray = { num1, num2, num3, num4, num5 };
            List<Array> result = new List<Array>();
            bool matchFound = false;
            Dictionary<int, int> arrayTracker = new Dictionary<int, int>();
            for (var i = 0; i < inputArray.Length; i++)
            {
                var currentEl = inputArray[i];
                arrayTracker[currentEl] = i;
            }

            for (var i = 0; i < inputArray.Length; i++)
            {
                var complement = kValue - inputArray[i];
                if (arrayTracker.ContainsKey(complement) && arrayTracker[complement] != i)
                {
                    matchFound = true;
                    // Remember to add the things you're expecting to the type you're expecting
                    int[] tempArray = { inputArray[arrayTracker[complement]], inputArray[i] };
                    result.Add(tempArray);
                    break;
                }
            }

            ViewData["Found"] = false; // Bool has to exist before casting magic

            if (matchFound)
            {
                ViewData["Found"] = true;
            }

            ViewData["Result"] = result;
            ViewData["kValue"] = kValue;
            ViewData["InputArray"] = inputArray;

            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
