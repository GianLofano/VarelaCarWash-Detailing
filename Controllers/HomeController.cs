using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VarelaCarWash.Models;
using VarelaCarWash.Filters;
using VarelaCarWash.Services;

namespace VarelaCarWash.Controllers
{
    [LoginRequired]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string? categoria)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == userId.Value);
                ViewBag.LoggedUser = user;
            }

            var productos = JsonProductoService.Leer();
            var servicios = JsonServicioService.Leer();
            var categorias = JsonCategoriaService.Leer();

            if (!string.IsNullOrEmpty(categoria) && categoria != "Todas")
            {
                productos = productos.Where(p => p.Categoria == categoria).ToList();
            }

            ViewBag.Categorias = categorias;

            var modelo = new HomeViewModel
            {
                Productos = productos,
                Servicios = servicios
            };

            return View(modelo);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    //Modelo para pasar datos a la vista
    public class HomeViewModel
    {
        public List<Product> Productos { get; set; } = new();
        public List<Service> Servicios { get; set; } = new();

        public string? CategoriaSeleccionada { get; set; }
        public List<string> CategoriasDisponibles { get; set; } = new();
    }
}
