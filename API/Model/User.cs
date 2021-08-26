﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyIssue.API.Model
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
        public string Password { get; set; }
        [Required]
        public decimal UserType { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual UserType UserTypes { get; set; }
    }
}