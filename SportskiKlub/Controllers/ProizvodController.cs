using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportskiKlub.Data;
using SportskiKlub.Models;

namespace SportskiKlub.Controllers
{
    public class ProizvodController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProizvodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proizvod
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Proizvod.Include(p => p.Inventar);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Proizvod/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvod
                .Include(p => p.Inventar)
                .FirstOrDefaultAsync(m => m.ProizvodID == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        // GET: Proizvod/Create
        public IActionResult Create()
        {
            ViewData["InventarID"] = new SelectList(_context.Inventar, "InventarID", "Naziv");
            return View();
        }

        // POST: Proizvod/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProizvodID,InventarID")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proizvod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventarID"] = new SelectList(_context.Inventar, "InventarID", "Naziv", proizvod.InventarID);
            return View(proizvod);
        }

        // GET: Proizvod/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod == null)
            {
                return NotFound();
            }
            ViewData["InventarID"] = new SelectList(_context.Inventar, "InventarID", "Naziv", proizvod.InventarID);
            return View(proizvod);
        }

        // POST: Proizvod/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ProizvodID,InventarID")] Proizvod proizvod)
        {
            if (id != proizvod.ProizvodID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proizvod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProizvodExists(proizvod.ProizvodID))
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
            ViewData["InventarID"] = new SelectList(_context.Inventar, "InventarID", "Naziv", proizvod.InventarID);
            return View(proizvod);
        }

        // GET: Proizvod/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvod
                .Include(p => p.Inventar)
                .FirstOrDefaultAsync(m => m.ProizvodID == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        // POST: Proizvod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod != null)
            {
                _context.Proizvod.Remove(proizvod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProizvodExists(long id)
        {
            return _context.Proizvod.Any(e => e.ProizvodID == id);
        }
    }
}
