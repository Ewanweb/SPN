using Microsoft.AspNetCore.Mvc;
using Site.Endpoint.Areas.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Endpoint.Areas.Blog.Controllers
{
    public class HomeController : AdminControllerBase
    {
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
