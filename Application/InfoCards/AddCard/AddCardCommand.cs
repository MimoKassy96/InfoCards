using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.InfoCards.AddCard
{
    /// <summary>
    /// Add Card Command - request command model for Mediatr handler
    /// </summary>
    public class AddCardCommand : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the info card id 
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
