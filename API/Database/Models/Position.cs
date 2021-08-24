using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Database.Models
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

        public ICollection<Employee> Employees { get; set; }
    }
}
