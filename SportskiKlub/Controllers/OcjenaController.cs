using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportskiKlub.Data;
using SportskiKlub.Models;

namespace SportskiKlub.Controllers
{
    [Authorize(Roles = "Administrator, Trener, Clan")]

    public class OcjenaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OcjenaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ocjena
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ocjena.ToListAsync());
        }

        // GET: Ocjena/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocjena = await _context.Ocjena
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocjena == null)
            {
                return NotFound();
            }

            return View(ocjena);
        }

        // GET: Ocjena/Create
        [Authorize(Roles = "Administrator, Clan")]

        public IActionResult Create()
        {
            // Filtriraj trenere iz baze gdje je VrstaKorisnika = 1
            var treneri = _context.Korisnik
                                  .Where(k => k.VrstaKorisnika == VrstaKorisnika.Trener) // Filtriraj trenere
                                  .Select(k => new SelectListItem
                                  {
                                      Value = k.Ime + " " + k.Prezime, // Kombiniraj ime i prezime
                                      Text = k.Ime + " " + k.Prezime
                                  })
                                  .ToList();

            ViewBag.Treneri = treneri; // Pošalji listu trenera u ViewBag
            return View();
        }

        // POST: Ocjena/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Clan")]


        public async Task<IActionResult> Create([Bind("Id,ImeTrenera,OcjenaTrenera,KorisnikID")] Ocjena ocjena)
        {

            _context.Add(ocjena);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Ocjena/Edit/5
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocjena = await _context.Ocjena.FindAsync(id);
            if (ocjena == null)
            {
                return NotFound();
            }
            return View(ocjena);
        }

        // POST: Ocjena/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,ImeTrenera,OcjenaTrenera,KorisnikID")] Ocjena ocjena)
        {
            if (id != ocjena.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocjena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcjenaExists(ocjena.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ocjena);
        }

        // GET: Ocjena/Delete/5
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocjena = await _context.Ocjena
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocjena == null)
            {
                return NotFound();
            }

            return View(ocjena);
        }

        // POST: Ocjena/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ocjena = await _context.Ocjena.FindAsync(id);
            if (ocjena != null)
            {
                _context.Ocjena.Remove(ocjena);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcjenaExists(int id)
        {
            return _context.Ocjena.Any(e => e.Id == id);
        }
    }
}
