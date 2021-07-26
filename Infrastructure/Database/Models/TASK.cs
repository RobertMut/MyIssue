namespace MyIssue.Infrastructure.Database.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TASKS")]
    public partial class TASK
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal taskId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal taskType { get; set; }

        [Required]
        [StringLength(100)]
        public string taskTitle { get; set; }

        public string taskDesc { get; set; }

        [StringLength(10)]
        public string taskOwner { get; set; }

        [StringLength(10)]
        public string taskAssignment { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? taskClient { get; set; }

        public DateTime? taskStart { get; set; }

        public DateTime? taskEnd { get; set; }

        public DateTime taskCreation { get; set; }

        [StringLength(100)]
        public string mailId { get; set; }

        public virtual CLIENT CLIENT { get; set; }

        public virtual EMPLOYEE EMPLOYEE { get; set; }

        public virtual EMPLOYEE EMPLOYEE1 { get; set; }

        public virtual TASKTYPE TASKTYPE1 { get; set; }
    }
}
