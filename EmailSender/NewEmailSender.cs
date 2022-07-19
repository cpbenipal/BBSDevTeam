using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace EmailSender
{
    public class NewEmailSender : INewEmailSender
    {
        public EmailHelperModel _emailSettings { get; set; } 
        public NewEmailSender(IOptions<EmailHelperModel> options)
        {
            this._emailSettings = options.Value;
        }

        public void SendEmail(
            string emailAddress,
            string subject,
            string message, bool Isadmin = false
        )
        {
            Execute(subject, message, emailAddress, Isadmin);
        }

        private void Execute(
            string subject,
            string message,
            string emailAddress, bool Isadmin 
        )
        {
            var smtpProvider = _emailSettings.EmailProvider;
            var portNumber = Convert.ToInt32(_emailSettings.PortNumber);
            var user = _emailSettings.User;
            var password = _emailSettings.Password;
            var sender = _emailSettings.EmailFrom;
            string EmailAddress = Isadmin ? _emailSettings.AdminEmail : emailAddress;
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", sender));
             
            emailMessage.To.Add(new MailboxAddress("", EmailAddress));
            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart("html")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {                
                client.Connect(smtpProvider, portNumber, SecureSocketOptions.Auto);
                client.Authenticate(user, password);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }

    }
}