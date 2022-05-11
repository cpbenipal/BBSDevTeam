namespace BBS.CustomExceptions
{
    public class UserAlreadyExistsException : RegisterUserException
    {
        public UserAlreadyExistsException(string Message)
            : base(Message)
        {
        }
    }
}