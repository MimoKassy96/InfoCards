using Application.Helpers;
using Application.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InfoCards.GetInfoCards
{
    public class GetInfoCardsHandler : IRequestHandler<GetInfoCardsQuery, List<InfoCardModel>>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetInfoCardsHandler> _logger;

        public GetInfoCardsHandler(ILogger<GetInfoCardsHandler> logger, IConfiguration config)
        {
            _configuration = config;
            _logger = logger;
        }

        public async Task<List<InfoCardModel>> Handle(GetInfoCardsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = new List<InfoCardModel>();

                using (var stream = File.OpenText(_configuration.GetSection(Helper.path).Value))
                {
                    var serializer = new JsonSerializer();

                    var listIfoCards = serializer.Deserialize(stream, typeof(List<JsonSerializeInfoCardModel>)) as List<JsonSerializeInfoCardModel>;

                    if (listIfoCards == null)
                        throw new InvalidCastException();

                    result = listIfoCards.Select(x => new InfoCardModel { Id = x.Id, Info = x.Info }).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't get info cards from file {ex}");
                throw;
            }
        }
    }
}
