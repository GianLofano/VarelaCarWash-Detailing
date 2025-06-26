using Microsoft.AspNetCore.Mvc;
using VarelaCarWash.Models;

namespace VarelaCarWash.Controllers
{
    public class ReservasController : Controller
    
    {
        private readonly ApplicationDbContext _context;

        public ReservasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var services = _context.Services.ToList();
            return View(services);
        }

        //[HttpPost]
        //public IActionResult Book(int serviceId, DateTime date)
        //{
        //    var userId = HttpContext.Session.GetInt32("UserId");
        //    if (userId == null) return RedirectToAction("Login", "Account");

        //    var Reservas = new Reservas
        //    {
        //        ServiceId = serviceId,
        //        AppointmentDate = date,
        //        UserId = userId.Value
        //    };

        //    _context.Reservas.Add(Reservas);
        //    _context.SaveChanges();

        //    return RedirectToAction("Confirm");
        //}

        public IActionResult Confirm()
        {
            return View();
        }
    }

}
