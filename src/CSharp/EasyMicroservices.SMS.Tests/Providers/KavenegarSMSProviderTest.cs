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

        async Task AppendService(params string[] ids)
        {
            await AppendService(KavenegarPort, $@"POST *RequestSkipBody* HTTP/1.1
*RequestSkipBody*Content-Length: *RequestSkipBody*

sender=*RequestSkipBody*&receptor={(ids.Length > 1 ? "09391111111%2C09391111112%2C09391111113" : "09391111111")}&message=*RequestSkipBody*&type=*RequestSkipBody*&date=*RequestSkipBody*"
 ,
 $@"HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
Server: Kestrel
X-Powered-By: ASP.NET
Date: Thu, 30 Apr 2020 05:05:43 GMT
Content-Length: 0

{{
	""return"": {{
        ""status"": 5,
        ""message"": ""ok""
    }},
	""entries"": [
	    {string.Join(",", ids.Select(x =>
 {
     return @$"{{
          ""messageid"": {x},
          ""cost"": 1200,
          ""gregorianDate"": ""2020-01-01T00:00:00"",
          ""date"": 1,
          ""message"": ""ok"",
          ""receptor"": null,
          ""sender"": null,
          ""status"": 5,
          ""statusText"": null
        }}";
 }))}
	]
}}");
        }

        public override async Task<SingleTextMessageResponse> SendSingleAsync(string message)
        {
            string id = "452453";
            await AppendService(id);
            var smsResult = await base.SendSingleAsync(message);
            Assert.Equal(id, smsResult.Id);
            return smsResult;
        }

        public override async Task<MultipleTextMessageResponse> SendMultipleAsync(string message)
        {
            string[] ids = new string[] { "452453", "452454" };
            await AppendService(ids);
            var smsResult = await base.SendMultipleAsync(message);
            Assert.True(smsResult.Ids.SequenceEqual(ids));
            return smsResult;
        }
    }
}
