using Newtonsoft.Json;
using System;
using System.Drawing;

namespace Application.Models
{
    /// <summary>
    /// Info card model for serialize and deserialize data from json file 
    /// </summary>
    public class JsonSerializeInfoCardModel
    {
        /// <summary>
        /// Gets or sets the info card Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the info it is description for Imaga on info card 
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// Gets or sets the Image Data from data file
        /// </summary>
        [JsonConverter(typeof(ImageConverter))]
        public Image ImageData { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
