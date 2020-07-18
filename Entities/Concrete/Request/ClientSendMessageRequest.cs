namespace Entities.Concrete.Request
{
    public class ClientSendMessageRequest : BaseRequest
    {
        public string SendMessage { get; set; }
        public string UserName { get; set; }
    }
}