using EasyMicroservices.SMS.Interfaces;
using EasyMicroservices.SMS.Models.Requests;
using EasyMicroservices.SMS.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public virtual async Task<SingleTextMessageResponse> SendAsync(SingleTextMessageRequest singleTextMessageRequest)
        {
            return (SingleTextMessageResponse)await SendAsync((MultipleTextMessageRequest)singleTextMessageRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipleTextMessageRequest"></param>
        /// <returns></returns>
        public Task<MultipleTextMessageResponse> SendAsync(MultipleTextMessageRequest multipleTextMessageRequest)
        {
            return ExceptionHelper.ExceptionHandler(async () =>
            {
                multipleTextMessageRequest.ThrowIfNull();
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
