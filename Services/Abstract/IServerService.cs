using Entities.Concrete.Request;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IServerService
    {
        /// <summary>
        /// Dinlemeyi Başlat
        /// </summary>
        bool StartListening(ServerStartListeningRequest baseRequest);

        /// <summary>
        /// Okumayı Başlat
        /// </summary>
        void StartReadAsync();

        /// <summary>
        /// Mesaj Gönder
        /// </summary>
        /// <param name="sendMessage"></param>
        /// <returns></returns>
        Task<bool> SendMessageAsync(ServerSendMessageRequest sendMessage);

        //Task<bool> StopServer();
    }
}