using Entities.Concrete.Request;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IServerService
    {
        /// <summary>
        /// Dinlemeyi Başlat
        /// </summary>
        bool StartListening(BaseRequest baseRequest);

        /// <summary>
        /// Okumayı Başlat
        /// </summary>
        Task StartReadAsync();

        /// <summary>
        /// Mesaj Gönder
        /// </summary>
        /// <param name="sendMessage"></param>
        /// <returns></returns>
        Task<bool> SendMessageAsync(ServerSendMessageRequest sendMessage);

        /// <summary>
        /// Server'ı Durdur
        /// </summary>
        /// <returns></returns>
        bool StopServer();
    }
}