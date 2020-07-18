namespace Entities.Constans
{
    public class Messages : IMessageEntity
    {
        /// <summary>
        /// Bağlantı kuruldu.
        /// </summary>
        public const string ConnectionEstablished = "Connection established!... \n";

        /// <summary>
        /// Sunucuya bağlanırken hata oluştu
        /// </summary>
        public const string ErrorWhileConnectingToServer = "Error while connecting to server! \n";

        /// <summary>
        /// Hata Oluştu
        /// </summary>
        public const string AnErrorOccurred = "An error occurred! \n";

        /// <summary>
        /// Dinleme Başlatıldı
        /// </summary>
        public const string ListeningStarted = "Listening started!... \n";

        /// <summary>
        /// Server Durduruldu!
        /// </summary>
        public const string ServerStopped = "Server stopped! \n";

        /// <summary>
        /// Server Durduruldu!
        /// </summary>
        public const string ServerCouldNotBeStopped = "Server could not be stopped! \n";

        /// <summary>
        /// İlk Bağlantı
        /// </summary>
        public static string FirstConnection = "First connection!";
    }
}