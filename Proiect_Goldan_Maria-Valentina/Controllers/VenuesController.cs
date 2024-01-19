using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_Goldan_Maria_Valentina.Data;
using Proiect_Goldan_Maria_Valentina.Models;
using Proiect_Goldan_Maria_Valentina.Models.LibraryViewModels;

namespace Proiect_Goldan_Maria_Valentina.Controllers
{
	[Authorize(Policy = "OnlySales")]
	public class VenuesController : Controller
    {
        private readonly LibraryContext _context;

        public VenuesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Venues
        public async Task<IActionResult> Index(int? id, int? concertID)
        {
            var VenueModel = new VenueIndexData();

            VenueModel.Venues = await _context.Venues
                .Include(i => i.VenueConcerts)
                    .ThenInclude(i => i.Concert)
                    .ThenInclude(i => i.Purchases)
                    .ThenInclude(i => i.Customer)
                    .ThenInclude(i => i.City)
                .Include(i => i.VenueConcerts)
                    .ThenInclude(i => i.Concert)
                    .ThenInclude(i => i.Artist)
                .AsNoTracking()
                .OrderBy(i => i.Name)
                .ToListAsync();

            if (id != null)
            {
                ViewData["VenueID"] = id.Value;
                Venue venue = VenueModel.Venues.Where(i => i.ID == id.Value).Single();
                VenueModel.Concerts = venue.VenueConcerts.Select(s => s.Concert);
            }
            if (concertID != null)
            {
                ViewData["ConcertID"] = concertID.Value;
                VenueModel.Purchases = VenueModel.Concerts.Where(x => x.ID == concertID).Single().Purchases;
            }
            return View(VenueModel);
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Venues == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.ID == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Adress")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Venues == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .Include(p => p.VenueConcerts)
                .ThenInclude(p => p.Concert)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (venue == null)
            {
                return NotFound();
            }

            PopulateVenueConcertData(venue);

            return View(venue);
        }

        private void PopulateVenueConcertData(Venue venue)
        {
            var allConcerts = _context.Concerts;
            var venueConcerts = new HashSet<int>(venue.VenueConcerts.Select(c => c.ConcertID));
            var viewModel = new List<VenueConcertData>();

            foreach (var concert in allConcerts)
            {
                viewModel.Add(new VenueConcertData
                {
                    ConcertID = concert.ID,
                    Name = concert.Name,
                    IsHeld = venueConcerts.Contains(concert.ID)
                });
            }

            ViewData["Concerts"] = viewModel;
        }
        private void UpdateVenueConcerts(string[] selectedConcerts, Venue venueToUpdate)
        {
            if (selectedConcerts == null)
            {
                venueToUpdate.VenueConcerts = new List<VenueConcert>();
                return;
            }

            var selectedConcertsHS = new HashSet<string>(selectedConcerts);
            var venueConcerts = new HashSet<int>(venueToUpdate.VenueConcerts.Select(c => c.Concert.ID));

            foreach (var concert in _context.Concerts)
            {
                if (selectedConcertsHS.Contains(concert.ID.ToString()))
                {
                    if (!venueConcerts.Contains(concert.ID))
                    {
                        venueToUpdate.VenueConcerts.Add(new VenueConcert { VenueID = venueToUpdate.ID, ConcertID = concert.ID });
                    }
                }
                else
                {
                    if (venueConcerts.Contains(concert.ID))
                    {
                        VenueConcert bookToRemove = venueToUpdate.VenueConcerts.FirstOrDefault(i => i.ConcertID == concert.ID);
                        _context.Remove(bookToRemove);
                    }
                }
            }
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Adress")] Venue venue, string[] selectedConcerts)
        {
            if (id != venue.ID)
            {
                return NotFound();
            }

            var venueToUpdate = await _context.Venues
                .Include(i => i.VenueConcerts)
                    .ThenInclude(i => i.Concert)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Venue>(venueToUpdate, "", i => i.Name, i => i.Adress))
            {
                UpdateVenueConcerts(selectedConcerts, venueToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }

            UpdateVenueConcerts(selectedConcerts, venueToUpdate);
            PopulateVenueConcertData(venueToUpdate);

            return View(venueToUpdate);
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Venues == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.ID == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Venues == null)
            {
                return Problem("Entity set 'LibraryContext.Venues'  is null.");
            }
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
            {
                _context.Venues.Remove(venue);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(int id)
        {
          return (_context.Venues?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
