using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    /// <summary>
    /// Upadate Card Params
    /// </summary>
    public class UpadateCardParams
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Gets or sets the Info
        /// </summary>
        public string info { get; set; }
        /// <summary>
        ///  Gets or sets the Path
        /// </summary>
        public string path { get; set; }
        /// <summary>
        ///  Gets or sets the File Name
        /// </summary>
        public string fileName { get; set; }
    }
}
