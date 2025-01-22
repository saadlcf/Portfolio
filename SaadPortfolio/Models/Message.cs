using System.ComponentModel.DataAnnotations.Schema;

namespace SaadPortfolio.Models
{
    [Table("Messages", Schema = "dbo")]

    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
    }
}
