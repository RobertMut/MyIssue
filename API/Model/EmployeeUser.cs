using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyIssue.API.Model
{
    [Table("EmployeeUser")]
    public class EmployeeUser
    {
        [Required]
        [StringLength(10)]
        public string UserLogin { get; set; }
        [Required]
        [StringLength(10)]
        public string EmployeeLogin { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
    }
}
