using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportskiKlub.Data;
using SportskiKlub.Models;

namespace SportskiKlub.Controllers
{
    public class GalerijaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalerijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Galerija
        public async Task<IActionResult> Index()
        {
            return View(await _context.Galerija.ToListAsync());
        }

        // GET: Galerija/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galerija = await _context.Galerija
                .FirstOrDefaultAsync(m => m.GalerijaID == id);
            if (galerija == null)
            {
                return NotFound();
            }

            return View(galerija);
        }

        // GET: Galerija/Create
        public IActionResult Create()
        {
            return View();
        }

        //CHAT
        // POST: Galerija/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalerijaID,naslov,DatotekaSlike")] Galerija galerija)
        {
            if (galerija.DatotekaSlike != null && galerija.DatotekaSlike.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    galerija.DatotekaSlike.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    galerija.naslov = "data:image/jpeg;base64," +
                        Convert.ToBase64String(fileBytes);
                    galerija.KorisnikID = 1;
                }
            }

            _context.Add(galerija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //// POST: Galerija/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("GalerijaID,naslov")] Galerija galerija)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(galerija);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(galerija);
        //}



        // GET: Galerija/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galerija = await _context.Galerija.FindAsync(id);
            if (galerija == null)
            {
                return NotFound();
            }
            return View(galerija);
        }

        // POST: Galerija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[Controller]/[Action]")]
        public async Task<IActionResult> Edit(long id, [Bind("GalerijaID,naslov")] Galerija galerija)
        {
            if (id != galerija.GalerijaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galerija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalerijaExists(galerija.GalerijaID))
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
            return View(galerija);
        }

        // GET: Galerija/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galerija = await _context.Galerija
                .FirstOrDefaultAsync(m => m.GalerijaID == id);
            if (galerija == null)
            {
                return NotFound();
            }

            return View(galerija);
        }

        // POST: Galerija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var galerija = await _context.Galerija.FindAsync(id);
            if (galerija != null)
            {
                _context.Galerija.Remove(galerija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalerijaExists(long id)
        {
            return _context.Galerija.Any(e => e.GalerijaID == id);
        }
    }
}
