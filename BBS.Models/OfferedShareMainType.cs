using BBS.Constants;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class OfferedShareMainType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
