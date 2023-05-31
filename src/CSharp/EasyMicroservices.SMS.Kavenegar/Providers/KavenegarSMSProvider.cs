using EasyMicroservices.SMS.Models.Requests;
using EasyMicroservices.SMS.Providers;
using Kavenegar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.SMS.Kavenegar.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class KavenegarSMSProvider : BaseSMSProvider
    {
        private readonly string _apiKey;
        private readonly string _apiAddress;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="apiAddress"></param>
        public KavenegarSMSProvider(string apiKey, string apiAddress = default)
        {
            apiKey.ThrowIfNull(nameof(apiKey));
            _apiKey = apiKey;
            _apiAddress = apiAddress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipleTextMessageRequest"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        protected override async Task<List<string>> ApiSendAsync(MultipleTextMessageRequest multipleTextMessageRequest)
        {
            if (_apiAddress.HasValue())
                KavenegarApi.BaseUrl = _apiAddress;
            KavenegarApi kavenegar = new KavenegarApi(_apiKey);


            var result = await kavenegar.Send(multipleTextMessageRequest.Senders.FirstOrEmptyException(),
                multipleTextMessageRequest.ToNumbers,
                multipleTextMessageRequest.Text);

            if (result.First().Status != 5)
            {
                throw new System.Exception($"Send sms error : {result.First().Message}");
            }

            return result.Select(x => x.Messageid.ToString()).ToList();
        }
    }
}
