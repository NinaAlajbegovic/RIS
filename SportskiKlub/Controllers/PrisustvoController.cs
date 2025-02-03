using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportskiKlub.Data;
using SportskiKlub.Models;

namespace SportskiKlub.Controllers
{
    [Authorize(Roles = "Administrator, Trener, Clan")]

    public class PrisustvoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrisustvoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prisustvo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prisustvo.ToListAsync());
        }

        // GET: Prisustvo/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prisustvo = await _context.Prisustvo
                .FirstOrDefaultAsync(m => m.PrisustvoID == id);
            if (prisustvo == null)
            {
                return NotFound();
            }

            return View(prisustvo);
        }

        // GET: Prisustvo/Create
        [Authorize(Roles = "Administrator, Trener")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Prisustvo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> Create([Bind("PrisustvoID,KorisnikID,TreningID,Prisutan")] Prisustvo prisustvo)
        {
            prisustvo.KorisnikID = 1;
            if (ModelState.IsValid)
            {
                _context.Add(prisustvo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prisustvo);
        }

        // GET: Prisustvo/Edit/5
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prisustvo = await _context.Prisustvo.FindAsync(id);
            if (prisustvo == null)
            {
                return NotFound();
            }
            return View(prisustvo);
        }

        // POST: Prisustvo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> Edit(long id, [Bind("PrisustvoID,KorisnikID,TreningID,Prisutan")] Prisustvo prisustvo)
        {
            if (id != prisustvo.PrisustvoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prisustvo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrisustvoExists(prisustvo.PrisustvoID))
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
            return View(prisustvo);
        }

        // GET: Prisustvo/Delete/5
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prisustvo = await _context.Prisustvo
                .FirstOrDefaultAsync(m => m.PrisustvoID == id);
            if (prisustvo == null)
            {
                return NotFound();
            }

            return View(prisustvo);
        }

        // POST: Prisustvo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var prisustvo = await _context.Prisustvo.FindAsync(id);
            if (prisustvo != null)
            {
                _context.Prisustvo.Remove(prisustvo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrisustvoExists(long id)
        {
            return _context.Prisustvo.Any(e => e.PrisustvoID == id);
        }
    }
}
