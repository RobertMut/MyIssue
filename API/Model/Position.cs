using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyIssue.API.Model
{
    [Table("Positions")]
    public class Position
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public decimal PositionId { get; set; }
        [Required]
        [StringLength(50)]
        public string PositionName { get; set; }
        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; }
    }
}
