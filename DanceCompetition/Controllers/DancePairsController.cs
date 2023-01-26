using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DanceCompetition.Data;
using DanceCompetition.Models;
using Microsoft.AspNetCore.Authorization;

namespace DanceCompetition.Controllers
{
    public class DancePairsController : Controller
    {
        private readonly DanceCompetitionContext _context;

        public DancePairsController(DanceCompetitionContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> IndexCompeting()
        {
            var viewModel = new DancePairViewModel
            {
                StillCompeting = _context.DancePair.Where(x => x.grade1 == 0 || x.grade2 == 0 || x.grade3 == 0).ToList()
            };
            return View(viewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> IndexResult()
        {
            var viewModel = new DancePairViewModel
            {
                FinishedContestants = _context.DancePair.Where(x => x.grade1 != 0 && x.grade2 != 0 && x.grade3 != 0).ToList(),
            };
            return View(viewModel);
        }

        [Authorize(Roles = SeedData.ROLE_ADMIN)]
        public IActionResult MissingGrades(int grade)
        {
            var viewModel = new DancePairViewModel();
            switch (grade)
            {
                case 1:
                    viewModel.MissingGrade1 = _context.DancePair.Where(x => x.grade1 == 0).ToList();
                    return View("MissingGrades_Grade1", viewModel);
                case 2:
                    viewModel.MissingGrade2 = _context.DancePair.Where(x => x.grade2 == 0).ToList();
                    return View("MissingGrades_Grade2", viewModel);
                case 3:
                    viewModel.MissingGrade3 = _context.DancePair.Where(x => x.grade3 == 0).ToList();
                    return View("MissingGrades_Grade3", viewModel);
                default:
                    return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SeedData.ROLE_ADMIN)]
        public async Task<IActionResult> ChangeGrade(int id, int grade, int grade_value)
        {
            var dancePair = await _context.DancePair
                .FirstOrDefaultAsync(m => m.Id == id);

            switch (grade)
            {
                case 1:
                    dancePair.grade1 = grade_value;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("MissingGrades", new { grade = 1 });
                case 2:
                    dancePair.grade2 = grade_value;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("MissingGrades", new { grade = 2 });
                case 3:
                    dancePair.grade3 = grade_value;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("MissingGrades", new { grade = 3 });
                default:
                    return View("Error");
            }
        }

        [Authorize(Roles = SeedData.ROLE_ADMIN)]
        public IActionResult AddDancePair()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SeedData.ROLE_ADMIN)]
        public IActionResult AddDancePair(DancePair dancePair)
        {
            if (ModelState.IsValid)
            {
                _context.DancePair.Add(dancePair);
                _context.SaveChanges();
                return RedirectToAction("IndexCompeting");
            }

            return View(dancePair);
        }


        //generated methods


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
        public async Task<IActionResult> Create([Bind("Id,name,grade1,grade2,grade3")] DancePair dancePair)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,grade1,grade2,grade3")] DancePair dancePair)
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
                return Problem("Entity set 'WebApplication2Context.DancePair'  is null.");
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

