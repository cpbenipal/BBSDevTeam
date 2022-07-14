using BBS.Services.Contracts;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BBS.Services.Repository
{
    public class TwilioSmsSender : ISmsSender
    {
        private readonly string _sid;
        private readonly string _apiKey;

        public TwilioSmsSender(string sid, string apiKey)
        {
            _sid = sid;
            _apiKey = apiKey;
        }

        public Task Send(string phoneNumber, string message)
        {
            TwilioClient.Init(_sid, _apiKey);

            var messageToSend = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber("+91-9888010144"),
                to: new Twilio.Types.PhoneNumber("+91-9888010144")
            );

            return Task.FromResult(messageToSend);
        }
    }
}
