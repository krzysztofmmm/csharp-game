using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_game.Models
{
    [Table("item")]
    public class Item
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public int ItemCost { get; set; }

        public int HealthBoost { get; set; }

        public int ManaBoost { get; set; }

        public int ExperienceBoost { get; set; }

        public int ResistanceBoost { get; set; }

        public int ArmorBoost { get; set; }
    }
}
