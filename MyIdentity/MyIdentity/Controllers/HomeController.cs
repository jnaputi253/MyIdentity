using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyIdentity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("Placeholder", "Placeholder");

            return View(dictionary);
        }
    }
}
