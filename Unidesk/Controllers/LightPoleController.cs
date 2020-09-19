using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Model;
using Domain;
using Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Unidesk.Controllers
{
    public class LightPoleController : Controller
    {

        private readonly ILightPoleApplication _appLightPole;
        private readonly ILocalizationApplication _appLocalization;

        public LightPoleController(ILightPoleApplication appLightPole, ILocalizationApplication appLocalization)
        {
            _appLightPole = appLightPole;
            _appLocalization = appLocalization;
        }

        public async Task<IActionResult> Index()
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];
            }
            var lightPoles = await _appLightPole.ListAsync();
            return View(lightPoles);
        }

        public async Task<IActionResult> Register(int id)
        {
            LoadData();
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            var model = await _appLocalization.GetByIdAsync(id);
            ViewBag.LocId = id;
            ViewBag.Localization = $"{ model.Address} - { model.Neighborhood} - { model.City}/{ model.State}";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LightPoleModel model)
        {
            LoadData();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _appLightPole.RegisterAsync(model);
                TempData["Success"] = true;
                TempData["Message"] = "Poste instalado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception x)
            {
                ViewBag.Success = false;
                ViewBag.Message = x.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            try
            {
                var model = await _appLightPole.GetByIdAsync(id);
                return View(model);
            }
            catch (Exception x)
            {
                ViewBag.Success = false;
                ViewBag.Message = x.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LightPoleModel model)
        {
            try
            {
                await _appLightPole.EditAsync(model);
                ViewBag.Success = true;
                ViewBag.Message = "Informações do poste de luz editadas";
                return View(model);
            }
            catch (Exception x)
            {
                ViewBag.Success = false;
                ViewBag.Message = x.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            try
            {
                var model = await _appLightPole.GetByIdAsync(id);
                return View(model);
            }
            catch (Exception x)
            {
                ViewBag.Success = false;
                ViewBag.Message = x.Message;
                return RedirectToAction("Index");
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
