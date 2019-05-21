using System;
using System.Collections.Generic;
using System.Text;

namespace Console
{
    public class ConfigModel
    {
        public string SourceFolder { get; set; }
        public string ErrorFolder { get; set; }
        public string SuccessFolder { get; set; }
        public string DestinationFolderId { get; set; }
        public string LogPath { get; set; }
    }
}
