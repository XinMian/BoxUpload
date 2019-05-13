using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxUpload.Models
{
    public class UploadFileViewModel
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string DeveloperToken { get; set; }
        public Uri uri { get; set; }
    }
}
