using System.Collections.Generic;

namespace EasyMicroservices.SMS.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TextMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> Senders { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }
    }
}
