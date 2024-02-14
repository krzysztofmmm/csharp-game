using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_game.Models
{
    [Table("class")]
    public class Class
    {
        public int Id { get; set; }

        public string ClassName { get; set; }

        public string Stats { get; set; } // This could be a JSON string
    }
}
