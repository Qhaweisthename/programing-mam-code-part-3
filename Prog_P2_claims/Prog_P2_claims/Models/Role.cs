using System.ComponentModel.DataAnnotations;

namespace Prog_P2_claims.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}