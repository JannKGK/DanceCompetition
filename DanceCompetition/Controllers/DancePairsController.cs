﻿using System.Linq;
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
        public async Task<IActionResult> IndexResult()
        {
            var viewModel = new DancePairViewModel
            {
                FinishedContestants = _context.DancePair.Where(x => x.grade1 != 0 && x.grade2 != 0 && x.grade3 != 0).ToList()
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

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
              return View(await _context.DancePair.ToListAsync());
        }

        [Authorize(Roles = SeedData.ROLE_ADMIN)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SeedData.ROLE_ADMIN)]
        public IActionResult Create(DancePair dancePair)
        {
            if (ModelState.IsValid)
            {
                _context.DancePair.Add(dancePair);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dancePair);
        }

    }
}
