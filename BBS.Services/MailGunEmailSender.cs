using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;

namespace BBS.Services.Repository
{
    public class MailGunEmailSender : IEmailSender
    {
        public MailGunSenderOptions _emailSettings { get; set; } 
        public MailGunEmailSender(IOptions<MailGunSenderOptions> options)
        {
            this._emailSettings = options.Value;
        }

        public async Task SendEmailAsync(
            string email,
            string subject,
            string message
        )
        {
            await Execute(subject, message, email);
        }

        private async Task Execute(
            string subject,
            string message,
            string email
        )
        {
            using var client = new HttpClient { BaseAddress = new Uri(_emailSettings.BaseUri) };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(_emailSettings.ApiKey)));

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("from", _emailSettings.From),
                new KeyValuePair<string, string>("to", email),
                new KeyValuePair<string, string>("subject", subject),
                new KeyValuePair<string, string>("text", message)
            });

            await client.PostAsync(_emailSettings.RequestUri, content).ConfigureAwait(false);
        }
    }
}