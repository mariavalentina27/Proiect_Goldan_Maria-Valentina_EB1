using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_Goldan_Maria_Valentina.Data;
using Proiect_Goldan_Maria_Valentina.Models;

namespace Proiect_Goldan_Maria_Valentina.Controllers
{
	[Authorize(Roles = "Employee")]
	public class ConcertsController : Controller
    {
        private readonly LibraryContext _context;

        public ConcertsController(LibraryContext context)
        {
            _context = context;
        }

		// GET: Concerts
		[AllowAnonymous]
		public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var concerts = from c in _context.Concerts select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                concerts = concerts.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    concerts = concerts.OrderByDescending(b => b.Name);
                    break;
                case "Price":
                    concerts = concerts.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    concerts = concerts.OrderByDescending(b => b.Price);
                    break;
                default:
                    concerts = concerts.OrderBy(b => b.Name);
                    break;
            }

            int pageSize = 2;

            return View(await PaginatedList<Concert>.CreateAsync(concerts.Include(b => b.Artist).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

		// GET: Concerts/Details/5
		[AllowAnonymous]
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Concerts == null)
            {
                return NotFound();
            }

            var concert = await _context.Concerts
                .Include (a => a.Artist)
                .Include (a => a.Purchases)
                    .ThenInclude (a => a.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (concert == null)
            {
                return NotFound();
            }

            return View(concert);
        }

        // GET: Concerts/Create
        public IActionResult Create()
        {
			ViewData["ArtistID"] = new SelectList(_context.Artists, "ID", "Name");
			return View();
        }

        // POST: Concerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ArtistID,Price")] Concert concert)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(concert);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException /* ex*/)
            {
				ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
			}

			ViewData["ArtistID"] = new SelectList(_context.Artists, "ID", "Name");
			return View(concert);
        }

        // GET: Concerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Concerts == null)
            {
                return NotFound();
            }

            var concert = await _context.Concerts.FindAsync(id);
            if (concert == null)
            {
                return NotFound();
            }
			ViewData["ArtistID"] = new SelectList(_context.Artists, "ID", "Name", concert.ArtistID);
			return View(concert);
        }

        // POST: Concerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var concertToUpdate = await _context.Concerts.FirstOrDefaultAsync(s => s.ID == id);

			if (await TryUpdateModelAsync<Concert>(concertToUpdate, "", s => s.ArtistID, s => s.Name, s => s.Price))
			{
				try
				{
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
				catch (DbUpdateException /* ex */)
				{
					ModelState.AddModelError("", "Unable to save changes. " +
					"Try again, and if the problem persists");
				}
			}
			return View(concertToUpdate);
		}

        // GET: Concerts/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Concerts == null)
            {
                return NotFound();
            }

            var concert = await _context.Concerts
                .Include(a => a.Artist)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (concert == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault()) 
            { 
                ViewData["ErrorMessage"] = "Delete failed. Try again"; 
            }

            return View(concert);
        }

        // POST: Concerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Concerts == null)
            {
                return Problem("Entity set 'LibraryContext.Concerts'  is null.");
            }
            var concert = await _context.Concerts.FindAsync(id);
            if (concert == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Concerts.Remove(concert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ConcertExists(int id)
        {
          return (_context.Concerts?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
