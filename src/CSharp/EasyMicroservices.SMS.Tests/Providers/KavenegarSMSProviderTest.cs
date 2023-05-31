using EasyMicroservices.SMS.Kavenegar.Providers;
using EasyMicroservices.SMS.Models.Responses;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EasyMicroservices.SMS.Tests.Providers
{
    public class KavenegarSMSProviderTest : BaseSMSProviderTest
    {
        const int KavenegarPort = 1402;
        public KavenegarSMSProviderTest() : base(new KavenegarSMSProvider("testapikey", $"http://localhost:{KavenegarPort}"))
        {

        }


        public override async Task<SingleTextMessageResponse> SendSingleAsync(string message)
        {
            string id = "452453";
            await SMSVirtualTestManager.AppendService(KavenegarPort, id);
            var smsResult = await base.SendSingleAsync(message);
            Assert.Equal(id, smsResult.Id);
            return smsResult;
        }

        public override async Task<MultipleTextMessageResponse> SendMultipleAsync(string message)
        {
            string[] ids = new string[] { "452453", "452454" };
            await SMSVirtualTestManager.AppendService(KavenegarPort, ids);
            var smsResult = await base.SendMultipleAsync(message);
            Assert.True(smsResult.Ids.SequenceEqual(ids));
            return smsResult;
        }
    }
}
