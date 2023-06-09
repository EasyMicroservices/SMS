﻿using EasyMicroservices.Laboratory.Constants;
using EasyMicroservices.SMS.Interfaces;
using EasyMicroservices.SMS.Models.Requests;
using EasyMicroservices.SMS.Models.Responses;
using EasyMicroservices.SMS.VirtualServerForTests;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace EasyMicroservices.SMS.Tests.Providers
{
    public abstract class BaseSMSProviderTest
    {
        public BaseSMSProviderTest(ISMSProvider sMSProvider)
        {
            SMSProvider = sMSProvider;
        }
        ISMSProvider SMSProvider { get; set; }

        protected static SMSVirtualTestManager SMSVirtualTestManager { get; set; } = new SMSVirtualTestManager();
        Task OnInitialize(int portNumber)
        {
            return SMSVirtualTestManager.OnInitialize(portNumber);
        }

        protected Task AppendService(int port, string request, string body)
        {
            return SMSVirtualTestManager.AppendService(port, request, body);
        }

        protected async Task<string> GetLastResponse(int port)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add(RequestTypeHeaderConstants.RequestTypeHeader, RequestTypeHeaderConstants.GiveMeLastFullRequestHeaderValue);
            var httpResponse = await httpClient.GetAsync($"http://localhost:{port}");
            return await httpResponse.Content.ReadAsStringAsync();
        }
        [Theory]
        [InlineData("Hello world")]
        [InlineData("Hello Ali")]
        [InlineData("Hello Madi")]
        public virtual async Task<SingleTextMessageResponse> SendSingleAsync(string message)
        {
            var smsResult = await SMSProvider.SendSingleAsync(new SingleTextMessageRequest()
            {
                Senders = new List<string>()
                {
                    "sender939"
                },
                Text = message,
                ToNumber = "09391111111"
            });

            Assert.True(smsResult.IsSuccess, await GetLastResponse(SMSVirtualTestManager.CurrentPortNumber));
            return smsResult;
        }
        [Theory]
        [InlineData("Hello world")]
        [InlineData("Hello Ali")]
        [InlineData("Hello Madi")]
        public virtual async Task<MultipleTextMessageResponse> SendMultipleAsync(string message)
        {
            var smsResult = await SMSProvider.SendMultipleAsync(new MultipleTextMessageRequest()
            {
                Senders = new List<string>()
                {
                    "senderMultiple939"
                },
                Text = message,
                ToNumbers = new List<string>()
                {
                    "09391111111",
                    "09391111112",
                    "09391111113"
                }
            });

            Assert.True(smsResult.IsSuccess, await GetLastResponse(SMSVirtualTestManager.CurrentPortNumber));
            return smsResult;
        }
    }
}
