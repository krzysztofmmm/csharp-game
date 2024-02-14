using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_game.Models
{
    [Table("token")]
    public class Token
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public string Value { get; set; }

        public DateTime ExpiryDate { get; set; }

        // Navigation property
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
