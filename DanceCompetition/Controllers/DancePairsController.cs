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

        public async Task<IActionResult> IndexResult()
        {
            return View(await _context.DancePair.ToListAsync());
        }

        public async Task<IActionResult> IndexGrade1()
        {
            var dancePairs = await _context.DancePair.ToListAsync();

            var filteredDancePairs = dancePairs.Where(dp => dp.grade1 == 0);

            return View(filteredDancePairs);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeGrade1(int id, int grade)
        {
            var dancePair = await _context.DancePair
                .FirstOrDefaultAsync(m => m.Id == id);
            dancePair.grade1 = grade;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexGrade1));
        }

        public async Task<IActionResult> ChangeGrade2(int id, int grade)
        {
            var dancePair = await _context.DancePair
                .FirstOrDefaultAsync(m => m.Id == id);
            dancePair.grade2 = grade;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexGrade2));
        }

        public async Task<IActionResult> ChangeGrade3(int id, int grade)
        {
            var dancePair = await _context.DancePair
                .FirstOrDefaultAsync(m => m.Id == id);
            dancePair.grade3 = grade;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexGrade3));
        }

        public async Task<IActionResult> Index()
        {
              return View(await _context.DancePair.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

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

    }
}
