using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoxUpload.Models;
using Box.V2.Config;
using Box.V2.Auth;
using Box.V2;
using Box.V2.Models;
using System.IO;
using System.Collections.Specialized;
using System.Web;

namespace BoxUpload.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Box box = new Box();
            box.JwtAuthen();

            UploadFileViewModel model = new UploadFileViewModel();
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
