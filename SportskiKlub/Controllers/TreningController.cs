using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportskiKlub.Data;
using SportskiKlub.Models;

namespace SportskiKlub.Controllers
{
    [Authorize(Roles = "Administrator, Trener, Clan")]

    public class TreningController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TreningController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trening
        [Authorize(Roles = "Administrator, Trener, Clan")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.Trening.ToListAsync());
        }

        // GET: Trening/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trening = await _context.Trening
                .FirstOrDefaultAsync(m => m.TreningID == id);
            if (trening == null)
            {
                return NotFound();
            }

            return View(trening);
        }

        // GET: Trening/Create
        [Authorize(Roles = "Administrator, Trener")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Trening/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[Controller]/[Action]")]
        public async Task<IActionResult> Create([Bind("TreningID,Datum,Vrijeme,Grupa,Vrsta")] Trening trening)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trening);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trening);
        }

        // GET: Trening/Edit/5
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trening = await _context.Trening.FindAsync(id);
            if (trening == null)
            {
                return NotFound();
            }
            return View(trening);
        }

        // POST: Trening/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> Edit(long id, [Bind("TreningID,Datum,Vrijeme,Grupa,Vrsta")] Trening trening)
        {
            if (id != trening.TreningID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trening);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreningExists(trening.TreningID))
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
            return View(trening);
        }

        // GET: Trening/Delete/5
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trening = await _context.Trening
                .FirstOrDefaultAsync(m => m.TreningID == id);
            if (trening == null)
            {
                return NotFound();
            }

            return View(trening);
        }

        // POST: Trening/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Trener")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var trening = await _context.Trening.FindAsync(id);
            if (trening != null)
            {
                _context.Trening.Remove(trening);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreningExists(long id)
        {
            return _context.Trening.Any(e => e.TreningID == id);
        }
    }
}
