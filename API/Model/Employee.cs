using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIssue.API.Model
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string EmployeeLogin { get; set; }
        
        [Required]
        [StringLength(70)]
        public string EmployeeName { get; set; }
        [Required]
        [StringLength(70)]
        public string EmployeeSurname { get; set; }
        [Required]
        [StringLength(15)]
        public string EmployeeNo { get; set; }
        [Required]
        public decimal EmployeePosition { get; set; }
        public virtual User EmployeeLogins { get; set; }
        public virtual Position Positions { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
