namespace BBS.CustomExceptions
{
    public class RegisterUserException : Exception
    {
        public RegisterUserException(string Message)
            : base(Message)
        {
        }
    }
}