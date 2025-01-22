using Microsoft.AspNetCore.Mvc;
using SaadPortfolio.Models;
using SaadPortfolio.Data;
using System.Linq;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly PortfolioDbContext _context;

    public HomeController(PortfolioDbContext context)
    {
        _context = context;
    }

    // Afficher la page d'accueil avec les projets, compétences, et expériences
    public IActionResult Index()
    {
        var projects = _context.Projects.ToList();
        var skills = _context.Skills.ToList();
        var experiences = _context.Experiences.ToList();

        // Convert string dates to DateTime if necessary
        foreach (var experience in experiences)
        {
            if (DateTime.TryParse(experience.StartDate?.ToString(), out DateTime startDate))
            {
                experience.StartDate = startDate;
            }
            else
            {
                experience.StartDate = null;
            }

            if (DateTime.TryParse(experience.EndDate?.ToString(), out DateTime endDate))
            {
                experience.EndDate = endDate;
            }
            else
            {
                experience.EndDate = null;
            }
        }

        ViewBag.Projects = projects;
        ViewBag.Skills = skills;
        ViewBag.Experiences = experiences;

        return View();
    }

    // Traiter l'envoi d'un message depuis le formulaire de contact
    [HttpPost]
    public async Task<IActionResult> Contact(Message message)
    {
        if (ModelState.IsValid)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            ViewBag.Message = "Merci pour votre message !";
        }
        return View("Index");
    }

    // Télécharger le CV
    public IActionResult DownloadResume()
    {
        var filePath = "wwwroot/files/Resume_Saad_Loucifi.pdf";
        var fileName = "Resume_Saad_Loucifi.pdf";
        return File(System.IO.File.ReadAllBytes(filePath), "application/pdf", fileName);
    }
}
