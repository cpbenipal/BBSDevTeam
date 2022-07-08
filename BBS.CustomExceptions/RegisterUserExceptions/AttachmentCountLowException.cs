namespace BBS.CustomExceptions
{
    public class AttachmentCountLowException : RegisterUserException
    {
        public AttachmentCountLowException(string Message)
            : base(Message)
        {
        }
    }
}