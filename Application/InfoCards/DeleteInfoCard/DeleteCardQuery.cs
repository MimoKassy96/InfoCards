using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.InfoCards.DeleteInfoCard
{
    /// <summary>
    /// Delete Card Query - request query model for Mediatr handler
    /// </summary>
    public class DeleteCardQuery : IRequest<IEnumerable<InfoCardModel>>
    {
        /// <summary>
        /// Gets or sets the info card Id
        /// </summary>
        public Guid id { get; set; }
    }
}
