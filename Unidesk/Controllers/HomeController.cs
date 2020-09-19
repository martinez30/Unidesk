using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Applications;
using Application.Interfaces;
using Application.Model;
using Domain;
using Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Unidesk.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
