using Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Client.Interfaces
{
    /// <summary>
    /// Info Cards Service
    /// </summary>
    public interface IInfoCardsService
    {
        /// <summary>
        /// Get Info Async
        /// </summary>
        /// <returns></returns>
        public Task<List<InfoCardModel>> GetInfoAsync();
        /// <summary>
        /// Upload Image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<BitmapImage> UploadImage(string id);
        /// <summary>
        /// Delete Info Card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<InfoCardModel>> DeleteInfoCard(string id);
        /// <summary>
        /// Add Card
        /// </summary>
        /// <param name="param"></param>
        public void AddCard(AddCardParams param);
        /// <summary>
        /// Upadate Card
        /// </summary>
        /// <param name="param"></param>
        public void UpadateCard(UpadateCardParams param);
    }
}
