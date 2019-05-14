using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Repository
{
    public interface IFileUploadRepository
    {
        List<FileUpload> Gets();
        FileUpload Get(int id);
        void Update(FileUpload item);
    }
}
