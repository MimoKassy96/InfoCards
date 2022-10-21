using System;

namespace Application.Models
{
    /// <summary>
    /// Info card model 
    /// </summary>
    public class InfoCardModel
    {
        /// <summary>
        /// Gets or sets the info  card Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the Info it is description for Imaga on info card 
        /// </summary>
        public string Info { get; set; }
    }
}
