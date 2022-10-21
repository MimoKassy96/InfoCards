using Application.Models;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    /// <summary>
    /// File Service
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Write to file
        /// </summary>
        /// <param name="listInfoCards"></param>
        public void WriteToFile(IEnumerable<JsonSerializeInfoCardModel> listInfoCards);
        /// <summary>
        /// Get all info cards from file
        /// </summary>
        /// <returns>List info cards</returns>
        public IEnumerable<JsonSerializeInfoCardModel> GetAllInfoCards();
        /// <summary>
        /// Get info card by id from file
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One info card</returns>
        public JsonSerializeInfoCardModel GetInfoCardById(Guid id);
    }
}
