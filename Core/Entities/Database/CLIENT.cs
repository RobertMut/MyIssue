namespace MyIssue.Core.Entities.Database
{ 
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CLIENTS")]
    public partial class CLIENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENT()
        {
            TASKS = new HashSet<TASK>();
        }

        [Column(TypeName = "numeric")]
        public decimal clientId { get; set; }

        [Required]
        [StringLength(250)]
        public string clientName { get; set; }

        [StringLength(3)]
        public string clientCountry { get; set; }

        [Required]
        [StringLength(20)]
        public string clientNo { get; set; }

        [StringLength(70)]
        public string clientStreet { get; set; }

        [StringLength(5)]
        public string clientStreetNo { get; set; }

        [StringLength(4)]
        public string clientFlatNo { get; set; }

        public string clientDesc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TASK> TASKS { get; set; }
    }
}
