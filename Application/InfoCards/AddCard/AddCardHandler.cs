using Application.Models;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Application.Exeption;
using System.Net;
using Application.Interfaces;

namespace Application.InfoCards.AddCard
{
    public class AddCardHandler : IRequestHandler<AddCardCommand, bool>
    {
        private readonly ILogger<AddCardHandler> _logger;
        private readonly IFileService _service;

        public AddCardHandler(ILogger<AddCardHandler> logger, IFileService service)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<bool> Handle(AddCardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Image == null || request.Image.Length == 0)
                    throw new RestException(HttpStatusCode.BadRequest);

                var listIfoCards = _service.GetAllInfoCards().ToList();

                using (var memoryStream = new MemoryStream())
                {
                    await request.Image.CopyToAsync(memoryStream);

                    var newInfoCard = new JsonSerializeInfoCardModel()
                    {
                        Id = request.id,
                        ImageData = Image.FromStream(memoryStream),
                        Info = request.info
                    };

                    listIfoCards.Add(newInfoCard);

                    _service.WriteToFile(listIfoCards);

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't add info card to file {ex}");
                return false;
            }
        }
    }
}
