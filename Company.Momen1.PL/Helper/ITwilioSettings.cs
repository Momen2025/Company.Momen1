using Company.Momen1.DAL.Sms;
using Twilio.Rest.Api.V2010.Account;

namespace Company.Momen1.PL.Helper
{
    public interface ITwilioSettings
    {
        public MessageResource SendSms(Sms sms);

    }
}
