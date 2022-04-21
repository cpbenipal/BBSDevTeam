using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Value { get; set; }
    }
}
