namespace BBS.Dto
{
    public class UserLoginDto
    {
        public int Id { get; set; }

        //public string Username { get; set; }
        //public string Password { get; set; }
        public string Passcode { get; set; }
        public int PersonId { get; set; }
    }
}
