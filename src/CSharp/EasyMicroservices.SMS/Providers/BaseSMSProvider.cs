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
        public virtual Task<SingleTextMessageResponse> SendSingleAsync(SingleTextMessageRequest singleTextMessageRequest)
        {
            return ExceptionHelper.ExceptionHandler(async () =>
            {
                var result = await SendMultipleAsync((MultipleTextMessageRequest)singleTextMessageRequest);
                return result.ToSingleTextMessageResponse(result.Ids?.FirstOrDefault());
            }, (ex) => ((MessageResponse)ex).ToSingleTextMessageResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipleTextMessageRequest"></param>
        /// <returns></returns>
        public Task<MultipleTextMessageResponse> SendMultipleAsync(MultipleTextMessageRequest multipleTextMessageRequest)
        {
            return ExceptionHelper.ExceptionHandler(async () =>
            {
                multipleTextMessageRequest.ThrowIfNull(nameof(multipleTextMessageRequest));
                return ((MessageResponse)true).ToMultipleTextMessageResponse(await ApiSendAsync(multipleTextMessageRequest));
            }, (ex) => ((MessageResponse)ex).ToMultipleTextMessageResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipleTextMessageRequest"></param>
        /// <returns></returns>
        protected abstract Task<List<string>> ApiSendAsync(MultipleTextMessageRequest multipleTextMessageRequest);
    }
}
