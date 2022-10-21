using Application.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.InfoCards.GetInfoCards
{
    /// <summary>
    /// Get Info Cards Query - request query model for Mediatr handler
    /// </summary>
    public class GetInfoCardsQuery : IRequest<List<InfoCardModel>>
    {
    }
}
