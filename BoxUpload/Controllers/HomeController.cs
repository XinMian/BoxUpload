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
using ApplicationCore.Repository;
using Infrastructure.Repository;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ApplicationCore.Service;

namespace BoxUpload.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<FileUpload> fileUploads = fileUploadRepository.Gets();

            Box box = new Box(fileUploadRepository);
            box.JwtAuthen(fileUploads);

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

        #region Dependency
        private readonly IFileUploadRepository fileUploadRepository;
        public HomeController(IFileUploadRepository fileUploadRepository)
        {
            this.fileUploadRepository = fileUploadRepository;
        }
        #endregion Dependency
    }


}
