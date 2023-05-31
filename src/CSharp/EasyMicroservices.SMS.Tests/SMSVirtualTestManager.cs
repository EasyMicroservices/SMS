using EasyMicroservices.Laboratory.Engine;
using EasyMicroservices.Laboratory.Engine.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.SMS.Tests
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
    }
}
