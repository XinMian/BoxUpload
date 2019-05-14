using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ApplicationCore.Service
{
    public class MyFile
    {
        public FileInfo[] GetFiles(string sourceFolder)
        {
            DirectoryInfo d = new DirectoryInfo(sourceFolder);//Assuming Test is your Folder
            FileInfo[] files = d.GetFiles("*.*"); //Getting Text files

            return files;
        }

        public List<FileUpload> ToFileUpload(FileInfo[] files, string dFolderId)
        {
            List<FileUpload> models = new List<FileUpload>();

            foreach (var item in files)
            {
                FileUpload model = new FileUpload();
                model.SPath = item.Directory.ToString();
                model.DName = item.Name;
                model.DFolderId = dFolderId;

                models.Add(model);
            }


            return models;
        }
    }
}
