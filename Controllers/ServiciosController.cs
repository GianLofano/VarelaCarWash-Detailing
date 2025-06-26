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
        public IActionResult Nuevo(Service servicio, IFormFile Imagen)
        {
            if (Imagen != null && Imagen.Length > 0)
            {
                var fileName = $"service_{Guid.NewGuid()}{Path.GetExtension(Imagen.FileName)}";
                var filePath = Path.Combine("wwwroot/images/services", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Imagen.CopyTo(stream);
                }
                servicio.ImagenPath = $"/images/services/{fileName}";
            }
            // Descripcion se toma del form
            servicio.Descripcion = Request.Form["Descripcion"];
            JsonServicioService.Agregar(servicio);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var servicio = JsonServicioService.ObtenerPorId(id);
            return View(servicio);
        }

        public IActionResult Detalle(int id)
        {
            var servicio = JsonServicioService.ObtenerPorId(id);
            if (servicio == null)
                return NotFound();
            return View(servicio);
        }

        [HttpPost]
        public IActionResult Editar(Service servicio, IFormFile Imagen)
        {
            if (Imagen != null && Imagen.Length > 0)
            {
                var fileName = $"service_{Guid.NewGuid()}{Path.GetExtension(Imagen.FileName)}";
                var filePath = Path.Combine("wwwroot/images/services", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Imagen.CopyTo(stream);
                }
                servicio.ImagenPath = $"/images/services/{fileName}";
            }
            // Descripcion se toma del form
            servicio.Descripcion = Request.Form["Descripcion"];
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
