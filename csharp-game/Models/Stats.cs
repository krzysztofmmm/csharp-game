using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_game.Models
{
    [Table("stats")]
    public class Stats
    {
        public int Id { get; set; }

        public int HealthPoints { get; set; }

        public int ManaPoints { get; set; }

        public int ExperiencePoints { get; set; }

        public int ResistancePoints { get; set; }

        public int ArmorPoints { get; set; }
    }
}
