using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public partial class FileUpload
    {
        public int Id { get; set; }
        public string SPath { get; set; }
        public string DName { get; set; }
        public string DFolderId { get; set; }
        public string DFileId { get; set; }
        public string ErrorLog { get; set; }
    }
}
