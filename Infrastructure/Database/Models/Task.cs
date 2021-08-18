﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyIssue.Infrastructure.Database.Models
{
    [Table("Tasks")]
    public class Task
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public decimal TaskId { get; set; }
        [Required]
        [StringLength(100)]
        public string TaskTitle { get; set; }
        public string TaskDesc { get; set; }
        public decimal TaskClient { get; set; }
        [StringLength(10)]
        public string TaskOwner { get; set; }
        [StringLength(10)]
        public string TaskAssignment { get; set; }
        public decimal TaskType { get; set; }
        public DateTime TaskStart { get; set; }
        public DateTime TaskEnd { get; set; }
        [Required]
        public DateTime TaskCreation { get; set; }
        [StringLength(100)]
        public string MailId { get; set; }
        public virtual Client Clients { get; set; }
        public virtual Employee Employees { get; set; }
        public virtual TaskType TaskTypes { get; set; }
    }
}