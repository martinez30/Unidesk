using Application.Applications;
using Application.Interfaces;
using Application.Model;
using Domain;
using Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Unidesk.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderApplication _appOrder;
        private readonly ILightPoleApplication _appLightPole;

        public OrderController(IOrderApplication appOrder,ILightPoleApplication appLightPole)
        {
            _appOrder = appOrder;
            _appLightPole = appLightPole;
        }
        public async Task<IActionResult> Index()
        {
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = TempData["Success"];
                ViewBag.Message = TempData["Message"];
            }
            var orders = await _appOrder.ListAsync();
            return View(orders);
        }
        public async Task<IActionResult> Register(int id)
        {
            LoadData();
            var model = await _appLightPole.GetByIdAsync(id);
            
            ViewBag.LightPoleId = id;
            ViewBag.Localization = $"{model.Localization}";
            ViewBag.SerialNumber = model.SerialNumber;
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateOrderModel model)
        {
            LoadData();
            model.Status = StatusOrder.Open;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _appOrder.RegisterAsync(model);
                TempData["Success"] = true;
                TempData["Message"] = "Ordem de Serviço Criada";
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
            LoadData();
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            try
            {
                var model = await _appOrder.GetByIdAsync(id);
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
        public async Task<IActionResult> Edit(OrderModel model)
        {
            LoadData();
            try
            {
                await _appOrder.EditAsync(model);
                ViewBag.Success = true;
                ViewBag.Message = "Informações da Ordem editadas";
                return View(model);
            }
            catch (Exception x)
            {
                ViewBag.Success = false;
                ViewBag.Message = x.Message;
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Finish(int id)
        {
            LoadData();
            if (TempData["Success"] as bool? ?? false)
            {
                ViewBag.Success = true;
                ViewBag.Message = TempData["Message"];
            }
            try
            {
                var model = await _appOrder.GetByIdAsync(id);
                ViewBag.Localization = model.Order.Localization;
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
        public async Task<IActionResult> Finish(OrderModel model)
        {
            LoadData();
            try
            {
                await _appOrder.Finish(model);
                TempData["Success"] = true;
                TempData["Message"] = "Ordem Finalizada";
                return RedirectToAction("Index");
            }
            catch (Exception x)
            {
                ViewBag.Success = false;
                ViewBag.Message = x.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var order = await _appOrder.GetByIdAsync(id);
                return View(order);
            }
            catch(Exception x)
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
