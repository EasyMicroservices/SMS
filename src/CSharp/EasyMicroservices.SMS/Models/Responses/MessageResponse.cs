using System;
using System.Collections.Generic;

namespace EasyMicroservices.SMS.Models.Responses
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ErrorResponse Error { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public static implicit operator MessageResponse(Exception exception)
        {
            return new MessageResponse()
            {
                IsSuccess = false,
                Error = new ErrorResponse()
                {
                    Message = exception.Message,
                    Details = exception.ToString()
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator MessageResponse(bool value)
        {
            return new MessageResponse()
            {
                IsSuccess = value
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public MultipleTextMessageResponse ToMultipleTextMessageResponse(List<string> ids = default)
        {
            return new MultipleTextMessageResponse()
            {
                Error = Error,
                IsSuccess = IsSuccess,
                Ids = ids
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SingleTextMessageResponse ToSingleTextMessageResponse(string id = default)
        {
            return new SingleTextMessageResponse()
            {
                Error = Error,
                IsSuccess = IsSuccess,
                Id = id
            };
        }
    }
}
