namespace EmailSender
{
    public interface INewEmailSender
    {
        void SendEmail(
            string emailAddress,
            string subject,
            string message
        );
    }
}