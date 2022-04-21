
using BBS.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{   
    [Table("UserRole")]
    public class UserRole : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        [ForeignKey("UserLoginId")]
        public int UserLoginId { get; set; }
        public UserLogin? UserLogin { get; set; }
    }
}
