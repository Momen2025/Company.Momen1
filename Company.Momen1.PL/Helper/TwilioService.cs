using Company.Momen1.DAL.Sms;
using Company.Momen1.PL.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Company.Momen1.PL.Helper
{
    public class TwilioService(IOptions<TwilioSettings> _options) : ITwilioSettings
    {
        public MessageResource SendSms(Sms sms)
        {
            //Initialize Connectio
            TwilioClient.Init(_options.Value.AccountSID, _options.Value.AuthToken);

            //Build Message
            var message = MessageResource.Create(
                body:sms.Body,
                to:sms.To,
                from:_options.Value.PhoneNumber
                //from:new Twilio.Types.PhoneNumber(_options.Value.PhoneNumber) // لو فى مشكله استعمل ده
            );
           //return Message
            return message;

        }
    }
}
