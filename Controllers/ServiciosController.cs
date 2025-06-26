using Microsoft.AspNetCore.Mvc;
using VarelaCarWash.Models;
using VarelaCarWash.Services;

namespace VarelaCarWash.Controllers
{
    public class ServiciosController : Controller
    {
        public IActionResult Index()
        {
            var servicios = JsonServicioService.Leer();
            return View(servicios);
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(Service servicio)
        {
            JsonServicioService.Agregar(servicio);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var servicio = JsonServicioService.ObtenerPorId(id);
            return View(servicio);
        }

        [HttpPost]
        public IActionResult Editar(Service servicio)
        {
            JsonServicioService.Editar(servicio);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            JsonServicioService.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}
