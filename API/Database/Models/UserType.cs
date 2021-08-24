using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIssue.Infrastructure.Database.Models
{
    [Table("UserTypes")]
    public class UserType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }
        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}