using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_game.Models
{
    [Table("character")]
    public class Character
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }

        public Class Class { get; set; }

        public string? Race { get; set; }

        public int TotalExperience { get; set; }

        public Stats Stats { get; set; }

        // Navigation property
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}