using Microsoft.AspNetCore.Mvc;
using VarelaCarWash.Models;
using VarelaCarWash.Services;

namespace VarelaCarWash.Controllers
{
    public class ProductosController : Controller
    {
        public IActionResult Index()
        {
            var productos = JsonProductoService.Leer();
            return View(productos);
        }

        public IActionResult Nuevo()
        {
            ViewBag.Categorias = JsonCategoriaService.Leer();
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(Product producto)
        {
            if (!ModelState.IsValid)
                return View(producto);

            JsonProductoService.Agregar(producto);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var producto = JsonProductoService.ObtenerPorId(id);
            ViewBag.Categorias = JsonCategoriaService.Leer();
            return View(producto);
        }

        [HttpPost]
        public IActionResult Editar(Product producto)
        {
            JsonProductoService.Editar(producto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            JsonProductoService.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}
