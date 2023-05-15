using System.Collections.Generic;

namespace EasyMicroservices.SMS.Models.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class MultipleTextMessageRequest : TextMessage
    {

        /// <summary>
        /// 
        /// </summary>
        public List<string> ToNumbers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="singleTextMessage"></param>
        public static explicit operator MultipleTextMessageRequest(SingleTextMessageRequest singleTextMessage)
        {
            return new MultipleTextMessageRequest()
            {
                Senders = singleTextMessage.Senders,
                Text = singleTextMessage.Text,
                ToNumbers = new List<string>() { singleTextMessage.ToNumber }
            };
        }
    }
}
