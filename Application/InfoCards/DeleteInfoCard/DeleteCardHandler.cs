using Application.Exeption;
using Application.Interfaces;
using Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InfoCards.DeleteInfoCard
{
    public class DeleteCardHandler : IRequestHandler<DeleteCardQuery, IEnumerable<InfoCardModel>>
    {
        private readonly ILogger<DeleteCardHandler> _logger;
        private readonly IFileService _service;

        public DeleteCardHandler(ILogger<DeleteCardHandler> logger, IFileService service)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IEnumerable<InfoCardModel>> Handle(DeleteCardQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.id == null)
                    throw new RestException(HttpStatusCode.BadRequest);

                var listInfoCards = _service.GetAllInfoCards().ToList();

                var result = listInfoCards.Where(c => c.Id != request.id);

                _service.WriteToFile(result);

                return result.Select(x => new InfoCardModel { Id = x.Id, Info = x.Info });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't delete info card from file {ex}");
                throw;
            }
        }
    }
}
