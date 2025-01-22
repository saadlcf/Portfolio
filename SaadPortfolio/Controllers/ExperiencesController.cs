using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaadPortfolio.Data;
using SaadPortfolio.Models;

namespace SaadPortfolio.Controllers
{
    public class ExperiencesController : Controller
    {
        private readonly SaadPortfolioContext _context;

        public ExperiencesController(SaadPortfolioContext context)
        {
            _context = context;
        }

        // GET: Experiences
        public async Task<IActionResult> Index()
        {
            var experiences = await _context.Experience.ToListAsync();
            return View(experiences);
        }

        // GET: Experiences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var experience = await _context.Experience
                .FirstOrDefaultAsync(e => e.Id == id.Value);

            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // GET: Experiences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Experiences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Company,Role,StartDate,EndDate,Description")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                // Validation des dates
                if (experience.StartDate.HasValue && experience.EndDate.HasValue && experience.StartDate > experience.EndDate)
                {
                    ModelState.AddModelError(string.Empty, "La date de début ne peut pas être supérieure à la date de fin.");
                    return View(experience);
                }

                _context.Experience.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(experience);
        }

        // GET: Experiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var experience = await _context.Experience.FindAsync(id.Value);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // POST: Experiences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Company,Role,StartDate,EndDate,Description")] Experience experience)
        {
            if (id != experience.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Validation des dates
                if (experience.StartDate.HasValue && experience.EndDate.HasValue && experience.StartDate > experience.EndDate)
                {
                    ModelState.AddModelError(string.Empty, "La date de début ne peut pas être supérieure à la date de fin.");
                    return View(experience);
                }

                try
                {
                    _context.Experience.Update(experience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(experience.Id))
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

            return View(experience);
        }

        // GET: Experiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var experience = await _context.Experience
                .FirstOrDefaultAsync(e => e.Id == id.Value);

            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experience = await _context.Experience.FindAsync(id);
            if (experience != null)
            {
                _context.Experience.Remove(experience);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ExperienceExists(int id)
        {
            return _context.Experience.Any(e => e.Id == id);
        }
    }
}
