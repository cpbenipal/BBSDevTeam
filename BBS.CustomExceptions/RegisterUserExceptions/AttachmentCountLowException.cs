namespace BBS.CustomExceptions.RegisterUserExceptions
{
    public class AttachmentCountLowException : RegisterUserException
    {
        public AttachmentCountLowException(string Message)
            : base(Message)
        {
        }
    }
}