using Application.Helpers;
using Application.Interfaces;
using Application.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Application.Services
{
    /// <summary>
    /// File Service
    /// </summary>
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        public FileService(IConfiguration config)
        {
            _configuration = config;
        }

        public IEnumerable<JsonSerializeInfoCardModel> GetAllInfoCards()
        {
            using (var stream = File.OpenText(_configuration.GetSection(Helper.path).Value))
            {
                var serializer = new JsonSerializer();

                var listIfoCards = serializer.Deserialize(stream, typeof(List<JsonSerializeInfoCardModel>)) as List<JsonSerializeInfoCardModel>;

                if (listIfoCards == null)
                    throw new InvalidCastException();

                return listIfoCards;
            }
        }

        public JsonSerializeInfoCardModel GetInfoCardById(Guid id)
        {
            using (var stream = File.OpenText(_configuration.GetSection(Helper.path).Value))
            {
                var serializer = new JsonSerializer();

                var listInfoCards = serializer.Deserialize(stream, typeof(List<JsonSerializeInfoCardModel>)) as List<JsonSerializeInfoCardModel>;

                if (listInfoCards == null)
                    throw new InvalidCastException();

                var result = listInfoCards.FirstOrDefault(x => x.Id == id);

                if (result == null)
                    throw new ArgumentNullException();

                return result;
            }
        }

        public void WriteToFile(IEnumerable<JsonSerializeInfoCardModel> listInfoCards)
        {
            using (var file = File.CreateText(_configuration.GetSection(Helper.path).Value))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, listInfoCards);
            }
        }
    }
}
