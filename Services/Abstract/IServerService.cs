using Entities.Concrete.Request;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IServerService
    {
        /// <summary>
        /// Dinlemeyi Başlat
        /// </summary>
        bool StartListening(StartListeningRequest baseRequest);

        /// <summary>
        /// Okumayı Başlat
        /// </summary>
        void StartReadAsync();

        /// <summary>
        /// Mesaj Gönder
        /// </summary>
        /// <param name="sendMessage"></param>
        /// <returns></returns>
        Task<bool> SendMessageAsync(SendMessageRequest sendMessage);

        //Task<bool> StopServer();
    }
}