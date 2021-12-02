using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyIssue.Main.API.Model
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
        [JsonIgnore]
        public EmployeeUser EmployeeUser { get; set; }
        [JsonIgnore]
        public virtual Position Positions { get; set; }
        [JsonIgnore]
        public ICollection<Task> Tasks { get; set; }
    }
}
