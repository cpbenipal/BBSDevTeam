namespace EmailSender
{
    public class EmailHelperModel
    {
        public string EmailProvider { get; set; }
        public string PortNumber { get; set; }
        public string Password { get; set; }
        public string User { get; set; }
        public string EmailFrom { get; set; }
        public string AdminEmail { get; set; } 
    }
}
