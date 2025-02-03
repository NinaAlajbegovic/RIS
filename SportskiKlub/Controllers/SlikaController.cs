using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportskiKlub.Data;
using SportskiKlub.Models;

namespace SportskiKlub.Controllers
{
    public class SlikaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SlikaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Slika
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Slika.Include(s => s.Galerija);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Slika/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slika = await _context.Slika
                .Include(s => s.Galerija)
                .FirstOrDefaultAsync(m => m.SlikaID == id);
            if (slika == null)
            {
                return NotFound();
            }

            return View(slika);
        }

        // GET: Slika/Create
        public IActionResult Create()
        {
            ViewData["GalerijaID"] = new SelectList(_context.Galerija, "GalerijaID", "GalerijaID");
            return View();
        }

        // POST: Slika/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SlikaID,GalerijaID,SlikaSadrzaj")] Slika slika)
        {
            if (ModelState.IsValid)
            {
                _context.Add(slika);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GalerijaID"] = new SelectList(_context.Galerija, "GalerijaID", "GalerijaID", slika.GalerijaID);
            return View(slika);
        }

        // GET: Slika/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slika = await _context.Slika.FindAsync(id);
            if (slika == null)
            {
                return NotFound();
            }
            ViewData["GalerijaID"] = new SelectList(_context.Galerija, "GalerijaID", "GalerijaID", slika.GalerijaID);
            return View(slika);
        }

        // POST: Slika/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SlikaID,GalerijaID,SlikaSadrzaj")] Slika slika)
        {
            if (id != slika.SlikaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slika);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlikaExists(slika.SlikaID))
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
            ViewData["GalerijaID"] = new SelectList(_context.Galerija, "GalerijaID", "GalerijaID", slika.GalerijaID);
            return View(slika);
        }

        // GET: Slika/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slika = await _context.Slika
                .Include(s => s.Galerija)
                .FirstOrDefaultAsync(m => m.SlikaID == id);
            if (slika == null)
            {
                return NotFound();
            }

            return View(slika);
        }

        // POST: Slika/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var slika = await _context.Slika.FindAsync(id);
            if (slika != null)
            {
                _context.Slika.Remove(slika);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlikaExists(long id)
        {
            return _context.Slika.Any(e => e.SlikaID == id);
        }
    }
}
