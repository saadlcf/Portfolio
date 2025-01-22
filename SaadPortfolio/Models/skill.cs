using System.ComponentModel.DataAnnotations.Schema;

namespace SaadPortfolio.Models
{
    [Table("Skills", Schema = "dbo")]

    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; } // Exemple : Débutant, Intermédiaire, Expert
    }

}
