using EasyMicroservices.Laboratory.Engine;
using EasyMicroservices.Laboratory.Engine.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.SMS.VirtualServerForTests
{
    public class SMSVirtualTestManager
    {
        static HashSet<int> InitializedPorts = new HashSet<int>();
        static readonly ResourceManager ResourceManager = new ResourceManager();
        public int CurrentPortNumber { get; set; }

        public async Task OnInitialize(int portNumber)
        {
            CurrentPortNumber = portNumber;
            if (InitializedPorts.Contains(portNumber))
                return;
            InitializedPorts.Add(portNumber);
            HttpHandler httpHandler = new HttpHandler(ResourceManager);
            await httpHandler.Start(portNumber);
        }

        public Task AppendService(int port, string request, string body)
        {
            ResourceManager.Append(request, body);
            return OnInitialize(port);
        }


        public async Task AppendService(int port, params string[] ids)
        {
            await AppendService(port, $@"POST *RequestSkipBody* HTTP/1.1
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
    }
}
