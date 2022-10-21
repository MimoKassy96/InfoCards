using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.InfoCards.UpdateCard
{
    /// <summary>
    /// Update Card Command - request command model for Mediatr handler
    /// </summary>
    public class UpdateCardCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the info card Id
        /// </summary>
        public Guid id { get; set; }
        /// <summary>
        /// Gets or sets the description for image
        /// </summary>
        public string info { get; set; }
        /// <summary>
        /// Gets or sets the image on info card 
        /// </summary>
        public IFormFile Image { get; set; }
    }
}
