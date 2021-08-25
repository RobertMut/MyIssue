using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIssue.API.Model
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string UserLogin { get; set; }
        [Required]
        [StringLength(128)]
        public string Password { get; set; }
        [Required]
        public decimal UserType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual UserType UserTypes { get; set; }
    }
}
