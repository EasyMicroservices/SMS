﻿using EasyMicroservices.SMS.Models.Requests;
using EasyMicroservices.SMS.Models.Responses;
using System.Threading.Tasks;

namespace EasyMicroservices.SMS.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISMSProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="singleTextMessageRequest"></param>
        /// <returns></returns>
        Task<SingleTextMessageResponse> SendSingleAsync(SingleTextMessageRequest singleTextMessageRequest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipleTextMessageRequest"></param>
        /// <returns></returns>
        Task<MultipleTextMessageResponse> SendMultipleAsync(MultipleTextMessageRequest multipleTextMessageRequest);
    }
}
