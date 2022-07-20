namespace BBS.CustomExceptions.RegisterUserExceptions
{

    public class EmiratesIDExistsException : RegisterUserException
    {
        public EmiratesIDExistsException(string Message)
            : base(Message)
        {
        }
    }
}