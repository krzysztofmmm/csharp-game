using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_game.Models
{
    [Table("inventory")]
    public class Inventory
    {
        public int Id { get; set; }

        [ForeignKey("Character")]
        public int CharacterId { get; set; }

        public Item Item { get; set; }

        // Navigation property
        public virtual Character Character { get; set; }
    }
}
