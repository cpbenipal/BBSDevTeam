using System;

namespace BBS.Dto
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public string Email { get; set; }        
        public DateTime DateOfBirth { get; set; }
    }
}
