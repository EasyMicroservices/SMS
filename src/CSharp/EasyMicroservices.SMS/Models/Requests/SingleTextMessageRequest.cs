namespace EasyMicroservices.SMS.Models.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class SingleTextMessageRequest : TextMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public string ToNumber { get; set; }
    }
}
