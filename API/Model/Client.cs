using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyIssue.API.Model
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ClientId { get; set; }
        [Required]
        [StringLength(250)]
        public string ClientName { get; set; }
        [StringLength(3)]
        public string ClientCountry { get; set; }
        [Required]
        [StringLength(20)]
        public string ClientNo { get; set; }
        [StringLength(70)]
        public string ClientStreet { get; set; }
        [StringLength(5)]
        public string ClientStreetNo { get; set; }
        [StringLength(4)]
        public string ClientFlatNo { get; set; }
        public string ClientDesc { get; set; }
        [JsonIgnore]
        public ICollection<Task> Tasks { get; set; }
        [JsonIgnore]
        public ICollection<ClientEmployee> ClientEmployees { get; set; }

    }
}
