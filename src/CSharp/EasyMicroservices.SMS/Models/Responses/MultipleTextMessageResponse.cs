using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyMicroservices.SMS.Models.Responses
{
    /// <summary>
    /// 
    /// </summary>
    public class MultipleTextMessageResponse : MessageResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> Ids { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multipleTextMessageResponse"></param>
        public static explicit operator SingleTextMessageResponse(MultipleTextMessageResponse multipleTextMessageResponse)
        {
            multipleTextMessageResponse.ThrowIfNull(nameof(multipleTextMessageResponse));
            return new SingleTextMessageResponse()
            {
                IsSuccess = multipleTextMessageResponse.IsSuccess,
                Id = multipleTextMessageResponse.Ids.First()
            };
        }
    }
}
