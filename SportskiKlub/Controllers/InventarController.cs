using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportskiKlub.Data;
using SportskiKlub.Models;

namespace SportskiKlub.Controllers
{
    [Authorize(Roles = "Administrator, Trener")]
    public class InventarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inventar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inventar.ToListAsync());
        }

        // GET: Inventar/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventar = await _context.Inventar
                .FirstOrDefaultAsync(m => m.InventarID == id);
            if (inventar == null)
            {
                return NotFound();
            }

            return View(inventar);
        }

        // GET: Inventar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[Controller]/[Action]")]
        public async Task<IActionResult> Create([Bind("InventarID,Naziv,Kolicina")] Inventar inventar)
        {
            inventar.KorisnikID = 1;
            _context.Add(inventar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Inventar/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventar = await _context.Inventar.FindAsync(id);
            if (inventar == null)
            {
                return NotFound();
            }
            return View(inventar);
        }

        // POST: Inventar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("InventarID,Naziv,Kolicina")] Inventar inventar)
        {
            if (id != inventar.InventarID)
            {
                return NotFound();
            }


            try
            {
                inventar.KorisnikID = 1;
                _context.Update(inventar);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarExists(inventar.InventarID))
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

        // GET: Inventar/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventar = await _context.Inventar
                .FirstOrDefaultAsync(m => m.InventarID == id);
            if (inventar == null)
            {
                return NotFound();
            }

            return View(inventar);
        }

        // POST: Inventar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var inventar = await _context.Inventar.FindAsync(id);
            if (inventar != null)
            {
                _context.Inventar.Remove(inventar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarExists(long id)
        {
            return _context.Inventar.Any(e => e.InventarID == id);
        }
    }
}
