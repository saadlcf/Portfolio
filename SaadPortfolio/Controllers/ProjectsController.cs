using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaadPortfolio.Data;
using SaadPortfolio.Models;

namespace SaadPortfolio.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly SaadPortfolioContext _context;

        public ProjectsController(SaadPortfolioContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            // Récupérer la liste de tous les projets de la base de données
            var projects = await _context.Project.ToListAsync();
            return View(projects);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Trouver un projet spécifique en utilisant son id
            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // Le binding permet de n'envoyer que les propriétés spécifiées de l'objet 'Project'
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Technology,Url")] Project project)
        {
            if (ModelState.IsValid)
            {
                // Ajouter un nouveau projet à la base de données
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Rediriger vers la page Index après l'ajout
            }
            return View(project); // Si le modèle est invalide, revenir à la vue de création
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Trouver le projet à modifier
            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project); // Retourner la vue avec le projet à modifier
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Technology,Url")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Mettre à jour le projet avec les nouvelles informations
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Vérifier si le projet existe toujours avant d'effectuer l'opération
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Rediriger après la mise à jour
            }
            return View(project); // Retourner la vue si le modèle est invalide
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Trouver le projet à supprimer
            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project); // Retourner la vue de suppression
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Trouver le projet à supprimer
            var project = await _context.Project.FindAsync(id);
            if (project != null)
            {
                _context.Project.Remove(project); // Supprimer le projet de la base de données
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Rediriger après la suppression
        }

        // Vérifier si le projet existe déjà
        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
