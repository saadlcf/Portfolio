using System.ComponentModel.DataAnnotations.Schema;
namespace SaadPortfolio.Models
{
    [Table("Projects", Schema = "dbo")]
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Technology { get; set; }
        public string Url { get; set; }
    }
}
