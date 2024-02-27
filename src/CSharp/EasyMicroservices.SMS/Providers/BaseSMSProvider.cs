using EasyMicroservices.ServiceContracts;
using EasyMicroservices.SMS.Interfaces;
using EasyMicroservices.SMS.Models.Requests;
using EasyMicroservices.SMS.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMicroservices.SMS.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseSMSProvider : ISMSProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="singleTextMessageRequest"></param>
        /// <returns></returns>
        public virtual async Task<MessageContract<SingleTextMessageResponse>> SendSingleAsync(SingleTextMessageRequest singleTextMessageRequest)
        {
            var result = await SendMultipleAsync((MultipleTextMessageRequest)singleTextMessageRequest);
            if (!result)
                return result.ToContract<SingleTextMessageResponse>();
            return (MessageContract<SingleTextMessageResponse>)(SingleTextMessageResponse)result.Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipleTextMessageRequest"></param>
        /// <returns></returns>
        public Task<MessageContract<MultipleTextMessageResponse>> SendMultipleAsync(MultipleTextMessageRequest multipleTextMessageRequest)
        {
            return ExceptionHelper.ExceptionHandler(async () =>
            {
                multipleTextMessageRequest.ThrowIfNull(nameof(multipleTextMessageRequest));
                return (MessageContract<MultipleTextMessageResponse>)new MultipleTextMessageResponse()
                {
                    Ids = await ApiSendAsync(multipleTextMessageRequest)
                };
            }, (ex) => ex.ToContract<MultipleTextMessageResponse>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipleTextMessageRequest"></param>
        /// <returns></returns>
        protected abstract Task<List<string>> ApiSendAsync(MultipleTextMessageRequest multipleTextMessageRequest);
    }
}
