using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyIssue.Main.API.Model
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
        [JsonIgnore]
        public virtual Client Clients { get; set; }
    }
}
