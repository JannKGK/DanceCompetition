using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DanceCompetition.Data;
using DanceCompetition.Models;

namespace DanceCompetition.Controllers
{
    public class DancePairsController : Controller
    {
        private readonly DanceCompetitionContext _context;

        public DancePairsController(DanceCompetitionContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexGrade1()
        {
            var dancePairs = await _context.DancePair.ToListAsync();

            var filteredDancePairs = dancePairs.Where(dp => dp.grade1 == 0);

            return View(filteredDancePairs);
        }

        public async Task<IActionResult> EditGrade1(int id)
        {
            var dancePair = await _context.DancePair.FindAsync(id);
            if (dancePair == null)
            {
                return NotFound();
            }

            return View(dancePair);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGrade1(int id, [Bind("Id,grade1")] DancePair dancePair)
        {
            if (id != dancePair.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingDancePair = await _context.DancePair.FindAsync(id);
                    existingDancePair.grade1 = dancePair.grade1;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DancePairExists(dancePair.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexGrade1));
            }
            return View(dancePair);
        }

        public async Task<IActionResult> IndexGrade2()
        {
            var dancePairs = await _context.DancePair.ToListAsync();

            var filteredDancePairs = dancePairs.Where(dp => dp.grade2 == 0);

            return View(filteredDancePairs);
        }

        public async Task<IActionResult> IndexGrade3()
        {
            var dancePairs = await _context.DancePair.ToListAsync();

            var filteredDancePairs = dancePairs.Where(dp => dp.grade3 == 0);

            return View(filteredDancePairs);
        }

            [HttpPost]
        public async Task<IActionResult> ChangeGrade1([Bind("Id,grade1")] DancePair dancePair)
        {
            var existingDancePair = await _context.DancePair
                .FirstOrDefaultAsync(m => m.Id == dancePair.Id);

            existingDancePair.grade1 = dancePair.grade1;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: DancePairs
        public async Task<IActionResult> Index()
        {
              return View(await _context.DancePair.ToListAsync());
        }

        // GET: DancePairs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DancePair == null)
            {
                return NotFound();
            }

            var dancePair = await _context.DancePair
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dancePair == null)
            {
                return NotFound();
            }

            return View(dancePair);
        }

        // GET: DancePairs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DancePairs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,grade1,grade2,grade3")] DancePair dancePair)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dancePair);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dancePair);
        }

        // GET: DancePairs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DancePair == null)
            {
                return NotFound();
            }

            var dancePair = await _context.DancePair.FindAsync(id);
            if (dancePair == null)
            {
                return NotFound();
            }
            return View(dancePair);
        }

        // POST: DancePairs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,grade1,grade2,grade3")] DancePair dancePair)
        {
            if (id != dancePair.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dancePair);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DancePairExists(dancePair.Id))
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
            return View(dancePair);
        }

        // GET: DancePairs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DancePair == null)
            {
                return NotFound();
            }

            var dancePair = await _context.DancePair
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dancePair == null)
            {
                return NotFound();
            }

            return View(dancePair);
        }

        // POST: DancePairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DancePair == null)
            {
                return Problem("Entity set 'DanceCompetitionContext.DancePair'  is null.");
            }
            var dancePair = await _context.DancePair.FindAsync(id);
            if (dancePair != null)
            {
                _context.DancePair.Remove(dancePair);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DancePairExists(int id)
        {
          return _context.DancePair.Any(e => e.Id == id);
        }
    }
}
