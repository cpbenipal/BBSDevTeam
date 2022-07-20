namespace BBS.CustomExceptions.RegisterUserExceptions
{
    public class RegisterUserException : Exception
    {
        public RegisterUserException(string Message)
            : base(Message)
        {
        }
    }
}