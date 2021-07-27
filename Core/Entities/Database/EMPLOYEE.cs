namespace MyIssue.Core.Entities.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EMPLOYEES")]
    public partial class EMPLOYEE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLOYEE()
        {
            TASKS = new HashSet<TASK>();
            TASKS1 = new HashSet<TASK>();
        }

        [Key]
        [StringLength(10)]
        public string employeeLogin { get; set; }

        [Required]
        [StringLength(70)]
        public string employeeName { get; set; }

        [Required]
        [StringLength(70)]
        public string employeeSurname { get; set; }

        [Required]
        [StringLength(15)]
        public string employeeNo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? employeePosition { get; set; }

        public virtual POSITION POSITION { get; set; }

        public virtual USER USER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TASK> TASKS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TASK> TASKS1 { get; set; }
    }
}
