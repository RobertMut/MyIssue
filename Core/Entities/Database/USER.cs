namespace MyIssue.Core.Entities.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USERS")]
    public partial class USER
    {
        [Key]
        [StringLength(10)]
        public string userLogin { get; set; }

        [Required]
        [StringLength(128)]
        public string password { get; set; }

        [Column(TypeName = "numeric")]
        public decimal type { get; set; }

        public virtual EMPLOYEE EMPLOYEE { get; set; }
    }
}
