using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaadPortfolio.Data;
using SaadPortfolio.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SaadPortfolio.Controllers
{
    public class SkillsController : Controller
    {
        private readonly SaadPortfolioContext _context;

        public SkillsController(SaadPortfolioContext context)
        {
            _context = context;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            var skills = await _context.Skill.ToListAsync();
            return View(skills);
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var skill = await _context.Skill
                .FirstOrDefaultAsync(s => s.Id == id.Value);

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // GET: Skills/Create
        public IActionResult Create()
        {
            // Initialisation d'un objet Skill vide avant de le passer à la vue
            return View(new Skill());
        }

        // POST: Skills/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Level")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                _context.Skill.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skill);
        }

        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var skill = await _context.Skill.FindAsync(id.Value);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Level")] Skill skill)
        {
            if (id != skill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Skill.Update(skill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skill.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(skill);
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var skill = await _context.Skill
                .FirstOrDefaultAsync(s => s.Id == id.Value);

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await _context.Skill.FindAsync(id);
            if (skill != null)
            {
                _context.Skill.Remove(skill);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Vérifie si une compétence existe dans la base de données
        private bool SkillExists(int id)
        {
            return _context.Skill.Any(s => s.Id == id);
        }
    }
}
