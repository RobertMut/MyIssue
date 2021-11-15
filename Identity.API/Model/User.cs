using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyIssue.Identity.API.Model
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
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        public decimal UserType { get; set; }
        [JsonIgnore]
        public EmployeeUser EmployeeUser { get; set; }
        [JsonIgnore]
        public virtual UserType UserTypes { get; set; }
    }
}
