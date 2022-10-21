using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Interfaces;

namespace Application.InfoCards.GetImage
{
    public class GetImageHandler : IRequestHandler<GetImageQuery, FileResult>
    {
        private readonly ILogger<GetImageHandler> _logger;
        private readonly IFileService _service;

        public GetImageHandler(ILogger<GetImageHandler> logger, IFileService service)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<FileResult> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var card = _service.GetInfoCardById(request.id);

                var ms = ImageToByte(card);

                var result = new FileContentResult(ms.ToArray(), Helper.typeToFile);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't get image from file {ex}");
                throw;
            }
        }

        private MemoryStream ImageToByte(JsonSerializeInfoCardModel card)
        {
            using (var ms = new MemoryStream())
            {
                ms.Flush();
                ms.Position = 0;
                card.ImageData.Save(ms, ImageFormat.Jpeg);

                return ms;
            }
        }
    }
}
