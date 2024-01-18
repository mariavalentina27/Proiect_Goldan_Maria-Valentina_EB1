using Microsoft.AspNetCore.Mvc;
using Proiect_Goldan_Maria_Valentina.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Proiect_Goldan_Maria_Valentina.Data;
using Proiect_Goldan_Maria_Valentina.Models.LibraryViewModels;

namespace Proiect_Goldan_Maria_Valentina.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LibraryContext _context;

        public HomeController(ILogger<HomeController> logger, LibraryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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

        public async Task<ActionResult> Statistics()
        {
            IQueryable<PurchaseGroup> data =
            from purchase in _context.Purchases
            group purchase by purchase.PurchaseDate into dateGroup
            select new PurchaseGroup()
            {
                PurchaseDate = dateGroup.Key,
                ConcertCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }
    }
}