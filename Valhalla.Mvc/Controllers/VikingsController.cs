using Microsoft.AspNetCore.Mvc;
using Valhalla.Mvc.Services;
using Valhalla.Shared.DTOs;

namespace Valhalla.Mvc.Controllers
{
    public class VikingsController : Controller
    {
        private readonly VikingApiService _service;

        public VikingsController(VikingApiService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var vikings = await _service.GetAll();

                return View(vikings);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error cargando datos: {ex.Message}";

                return View(new List<VikingDto>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            VikingDto model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Debe completar todos los campos";

                return View(model);
            }

            try
            {
                await _service.Create(model);

                TempData["Success"] = "Vikingo registrado correctamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error guardando vikingo: {ex.Message}";

                return View(model);
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.Delete(id);

                TempData["Success"] = "Vikingo eliminado correctamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error eliminando: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var viking = await _service.GetById(id);

                if (viking == null)
                {
                    TempData["Error"] = "Vikingo no encontrado";

                    return RedirectToAction(nameof(Index));
                }

                return View(viking);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error cargando registro: {ex.Message}";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            VikingDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _service.Update(model);

                TempData["Success"] = "Vikingo actualizado correctamente";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error actualizando: {ex.Message}";

                return View(model);
            }
        }
    }
}