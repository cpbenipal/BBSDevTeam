using SendGrid;
using SendGrid.Helpers.Mail;

namespace BBS.Services.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(
            string emailAddress, 
            string subject, 
            string message, 
            List<Attachment>? attachments = null
        );
    }
}
