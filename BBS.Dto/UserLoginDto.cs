namespace BBS.Dto
{
    public class UserLoginDto
    { 
        public string Username { get; set; }
        public string Password { get; set; }
        public int PersonId { get; set; }
        public int RoleId { get; set; }
    }
}
