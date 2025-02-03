using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportskiKlub.Data;
using SportskiKlub.Models;

namespace SportskiKlub.Controllers
{
    [Authorize(Roles = "Administrator, Trener, Clan")]

    public class ClanarinaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClanarinaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clanarina
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.Clanarina.ToListAsync());
        }

        // GET: Clanarina/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clanarina = await _context.Clanarina
                .FirstOrDefaultAsync(m => m.ClanarinaID == id);
            if (clanarina == null)
            {
                return NotFound();
            }

            return View(clanarina);
        }

        // GET: Clanarina/Create
        [Authorize(Roles = "Administrator, Trener")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Clanarina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[Controller]/[Action]")]
        public async Task<IActionResult> Create([Bind("ClanarinaID, DatumUplate, Iznos")] Clanarina clanarina)
        {

            clanarina.KorisnikID = 1;
            _context.Add(clanarina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Clanarina/Edit/5
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clanarina = await _context.Clanarina.FindAsync(id);
            if (clanarina == null)
            {
                return NotFound();
            }
            return View(clanarina);
        }

        // POST: Clanarina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> Edit(long id, [Bind("ClanarinaID,DatumUplate,Iznos")] Clanarina clanarina)
        {
            if (id != clanarina.ClanarinaID)
            {
                return NotFound();
            }


            try
            {
                clanarina.KorisnikID = 2;
                _context.Update(clanarina);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClanarinaExists(clanarina.ClanarinaID))
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

        // GET: Clanarina/Delete/5
        [Authorize(Roles = "Administrator, Trener")]

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clanarina = await _context.Clanarina
                .FirstOrDefaultAsync(m => m.ClanarinaID == id);
            if (clanarina == null)
            {
                return NotFound();
            }

            return View(clanarina);
        }

        // POST: Clanarina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Trener")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var clanarina = await _context.Clanarina.FindAsync(id);
            if (clanarina != null)
            {
                _context.Clanarina.Remove(clanarina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClanarinaExists(long id)
        {
            return _context.Clanarina.Any(e => e.ClanarinaID == id);
        }
    }
}
