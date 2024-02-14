using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_game.Models
{
    [Table("applicationuser")]
    public class ApplicationUser : IdentityUser
    {
        [Column("id")]
        public override string Id { get; set; }

        [Column("username")]
        public override string UserName { get; set; }

        [Column("email")]
        public override string Email { get; set; }

        [Column("passwordhash")]
        public override string PasswordHash { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }
    }
}
