using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_game.Models
{
    [Table("monster")]
    public class Monster
    {
        public int Id { get; set; }

        public string MonsterName { get; set; }

        public Stats Stats { get; set; } // This could be a JSON string
    }
}
