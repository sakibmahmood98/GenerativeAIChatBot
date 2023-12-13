using System.ComponentModel.DataAnnotations.Schema;

namespace GenerativeAIChatBot.Models.Entities
{
    public class History
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
