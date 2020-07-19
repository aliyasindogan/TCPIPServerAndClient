namespace Entities.Constans
{
    public class Messages : IMessageEntity
    {
        /// <summary>
        /// Bağlantı kuruldu.
        /// </summary>
        public const string ConnectionEstablished = "Bağlantı kuruldu!... \n";

        /// <summary>
        /// Sunucuya bağlanırken hata oluştu
        /// </summary>
        public const string ErrorWhileConnectingToServer = "Sunucuya bağlanırken hata oluştu! \n";

        /// <summary>
        /// Hata oluştu
        /// </summary>
        public const string AnErrorOccurred = "Hata oluştu! \n";

        /// <summary>
        /// Dinleme başlatıldı
        /// </summary>
        public const string ListeningStarted = " Dinleme Başlatıldı!... \n";

        /// <summary>
        /// Server durduruldu!
        /// </summary>
        public const string ServerStopped = "Server durduruldu! \n";

        /// <summary>
        /// Server durdurulamadı!
        /// </summary>
        public const string ServerCouldNotBeStopped = "Server durdurulamadı! \n";

        /// <summary>
        /// İlk Bağlantı
        /// </summary>
        public static string FirstConnection = "First connection!";

        public static string Disconnection = "Bağlantı durduruldu!";
    }
}