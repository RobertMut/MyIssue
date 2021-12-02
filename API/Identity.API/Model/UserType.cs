using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyIssue.Identity.API.Model
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
        [JsonIgnore]
        public ICollection<User> Users { get; set; }
    }
}