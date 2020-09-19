using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Model;
using Domain;
using Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Unidesk.Controllers
{
    public class LocalizationController : Controller
    {
        private readonly ILocalizationApplication _appLocalization;

        public LocalizationController(ILocalizationApplication appLocalization)
        {
            _appLocalization = appLocalization;
        }

        public async Task<IActionResult> Index()
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];
            }
            var localizations = await _appLocalization.ListAsync();
            return View(localizations);
        }

        public IActionResult Register()
        {
            LoadData();
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LocalizationModel model)
        {
            LoadData();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _appLocalization.RegisterAsync(model);
                TempData["Success"] = true;
                TempData["Message"] = "Localização cadastrada com sucesso";
                return RedirectToAction("Index");
            }
            catch(Exception x)
            {
                ViewBag.Success = false;
                ViewBag.Message = x.Message;
                return View(model);
            }
        }

        private void LoadData()
        {
            var problems = Enum.GetValues(typeof(Problems)).Cast<Problems>().Select(x => new SelectListItem { Text = x.GetDescription(), Value = ((int)x).ToString() });
            ViewBag.Problems = problems;
            var states = Enum.GetValues(typeof(State)).Cast<State>().Select(x => new SelectListItem { Text = x.GetDescription(), Value = ((int)x).ToString() });
            ViewBag.States = states;
        }
    }
}
