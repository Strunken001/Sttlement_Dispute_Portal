using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Controllers
{
    public class RatingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
