 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace BBS.Models
{  
    [Table("CertificateType")]
    public class CertificateType 
    { 
        [Key]
        public string? Id { get; set; } 
        public string? TypeName { get; set; } 
        public string? Description { get; set; } 
    }
}

