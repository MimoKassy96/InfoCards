using System;

namespace Client.Models
{
    /// <summary>
    /// Info card model 
    /// </summary>
    public class InfoCardModel
    {
        // Info card Id
        public Guid Id { get; set; }
        // Description for Imaga on info card 
        public string Info { get; set; }
    }
}
