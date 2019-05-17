using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Service
{
    public class FileUploadService
    {
        private List<FileUpload> fileUploads;

        public FileUploadService(List<FileUpload> fileUploads)
        {
            this.fileUploads = fileUploads;
        }

        public int countUnSuccessFile()
        {
            int count = (fileUploads.Where(x => string.IsNullOrEmpty(x.DFileId) && string.IsNullOrEmpty(x.ErrorLog)).ToList()).Count;

            return count;
        }
    }
}
