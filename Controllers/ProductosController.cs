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
        public IActionResult Nuevo(Product producto, IFormFile Imagen)
        {
            if (Imagen != null && Imagen.Length > 0)
            {
                var fileName = $"product_{Guid.NewGuid()}{Path.GetExtension(Imagen.FileName)}";
                var filePath = Path.Combine("wwwroot/images/products", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Imagen.CopyTo(stream);
                }
                producto.ImagenPath = $"/images/products/{fileName}";
            }
            if (!ModelState.IsValid)
                return View(producto);

            // Descripcion se toma del form
            producto.Descripcion = Request.Form["Descripcion"];

            JsonProductoService.Agregar(producto);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var producto = JsonProductoService.ObtenerPorId(id);
            ViewBag.Categorias = JsonCategoriaService.Leer();
            return View(producto);
        }

        public IActionResult Detalle(int id)
        {
            var producto = JsonProductoService.ObtenerPorId(id);
            if (producto == null)
                return NotFound();
            return View(producto);
        }

        [HttpPost]
        public IActionResult Editar(Product producto, IFormFile Imagen)
        {
            if (Imagen != null && Imagen.Length > 0)
            {
                var fileName = $"product_{Guid.NewGuid()}{Path.GetExtension(Imagen.FileName)}";
                var filePath = Path.Combine("wwwroot/images/products", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Imagen.CopyTo(stream);
                }
                producto.ImagenPath = $"/images/products/{fileName}";
            }
            // Descripcion se toma del form
            producto.Descripcion = Request.Form["Descripcion"];

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
