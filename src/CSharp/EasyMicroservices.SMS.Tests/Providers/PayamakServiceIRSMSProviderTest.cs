using EasyMicroservices.SMS.Kavenegar.Providers;
using EasyMicroservices.SMS.Models.Responses;
using EasyMicroservices.SMS.PayamakServiceIR.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EasyMicroservices.SMS.Tests.Providers
{
    //public class PayamakServiceIRSMSProviderTest : BaseSMSProviderTest
    //{
    //    public PayamakServiceIRSMSProviderTest() : base(new PayamakServiceIRProvider
    //        ("us", $"pass"))
    //    {

    //    }


    //    public override async Task<SingleTextMessageResponse> SendSingleAsync(string message)
    //    {
    //        try
    //        {
    //            var smsResult = await SMSProvider.SendSingleAsync(new Models.Requests.SingleTextMessageRequest()
    //            {
    //                Senders = new List<string>()
    //            {
    //                "+1111111110000000"
    //            },
    //                Text = message,
    //                ToNumber = "9999"
    //            });

    //            Assert.True(smsResult.IsSuccess, await GetLastResponse(SMSVirtualTestManager.CurrentPortNumber));
    //            return smsResult;
    //        }
    //        catch (System.Exception exaa)
    //        {
    //            var qqq = exaa;
    //            throw;
    //        }
    //    }
    //}
}
