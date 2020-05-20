using System;
using System.Collections.Generic;
using InsideAirbnbApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsideAirbnbApp.Controllers
{
    public class AdminController : Controller
    {
        [Authorize (Policy = "Admin")]
        public ActionResult Index()
        {
            var lstModel = new List<DefaultChartViewModel>();
            
            return View(lstModel);
        }
    }
}
