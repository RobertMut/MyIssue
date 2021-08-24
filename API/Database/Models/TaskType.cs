using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Database.Models
{
    [Table("TaskTypes")]
    public class TaskType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public decimal TypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
