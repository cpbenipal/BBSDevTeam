namespace BBS.CustomExceptions
{

    public class EmiratesIDExistsException : RegisterUserException
    {
        public EmiratesIDExistsException(string Message)
            : base(Message)
        {
        }
    }
}