using Application.Exeption;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InfoCards.UpdateCard
{
    public class UpdateCardHandler : IRequestHandler<UpdateCardCommand, bool>
    {
        private readonly ILogger<UpdateCardHandler> _logger;
        private readonly IFileService _service;

        public UpdateCardHandler(ILogger<UpdateCardHandler> logger, IFileService service)
        {
            _service = service;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Image == null || request.Image.Length == 0)
                    throw new RestException(HttpStatusCode.BadRequest);

                var listInfoCards =  _service.GetAllInfoCards();

                using (var memoryStream = new MemoryStream())
                {
                    await request.Image.CopyToAsync(memoryStream);

                    foreach (var card in listInfoCards)
                    {
                        if (card.Id == request.id)
                        {
                            await request.Image.CopyToAsync(memoryStream);
                            card.Info = request.info;
                            card.ImageData = Image.FromStream(memoryStream);
                        }
                    }

                    _service.WriteToFile(listInfoCards);

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't update info card on the file {ex}");
                return false;
            }
        }
    }
}
