using System.ComponentModel.DataAnnotations.Schema;

namespace SaadPortfolio.Models
{
    [Table("Experiences", Schema = "dbo")]

    public class Experience
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Role { get; set; }
        public DateTime? StartDate { get; set; } // Nullable DateTime
        public DateTime? EndDate { get; set; }   // Nullable DateTime
        public string Description { get; set; }
    }

}

