using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIssue.API.Model
{
    [Table("ClientEmployees")]
    public class ClientEmployee
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal EmployeeId { get; set; }
        [Required]
        [StringLength(70)]
        public string Name { get; set; }
        [Required]
        [StringLength(80)]
        public string Surname { get; set; }
        public decimal Client { get; set; }
        public virtual Client Clients { get; set; }
    }
}
