using Entities.Concrete.Request;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IClientService
    {
        /// <summary>
        /// Server!e Bağlan ve Okmayı Başlat
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        Task ConnectionAndStartReadyAsync(BaseRequest baseRequest);

        /// <summary>
        /// Mesaj Gönder
        /// </summary>
        /// <param name="sendMessage"></param>
        /// <returns></returns>
        Task<bool> SendMessageAsync(ClientSendMessageRequest sendMessage);

        void Disconnect();
    }
}