namespace BBS.CustomExceptions.RegisterUserExceptions
{
    public class UserAlreadyExistsException : RegisterUserException
    {
        public UserAlreadyExistsException(string Message)
            : base(Message)
        {
        }
    }
}