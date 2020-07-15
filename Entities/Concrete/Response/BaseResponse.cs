namespace Entities.Concrete.Response
{
    public class BaseResponse
    {
        /// <summary>
        /// Gelen Mesaj
        /// </summary>
        public string GetMessage { get; set; }

        /// <summary>
        /// Hata Mesajı
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Başarılı Mesajı
        /// </summary>
        public string SuccessMessage { get; set; }

        /// <summary>
        /// Durum true ,false
        /// </summary>
        public bool Status { get; set; }
    }
}