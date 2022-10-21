using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Application.InfoCards.GetImage
{
    /// <summary>
    /// Get Image Query - request query model for Mediatr handler
    /// </summary>
    public class GetImageQuery : IRequest<FileResult>
    {
        /// <summary>
        /// Gets or sets the info card Id
        /// </summary>
        public Guid id { get; set; }
    }
}
