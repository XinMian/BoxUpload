using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApplicationCore.Service
{
    public class MyFile
    {
        public List<FileUpload> GetFiles(string sourceFolder, string dFolderId)
        {
            DirectoryInfo d = new DirectoryInfo(sourceFolder);//Assuming Test is your Folder
            FileInfo[] files = d.GetFiles("*.*"); //Getting Text files

            return ToFileUpload(files, dFolderId);
        }

        private List<FileUpload> ToFileUpload(FileInfo[] files, string dFolderId)
        {
            List<FileUpload> models = new List<FileUpload>();

            foreach (var item in files)
            {
                FileUpload model = new FileUpload();
                model.SPath = item.FullName.ToString();
                model.DName = item.Name;
                model.DFolderId = dFolderId;

                models.Add(model);
            }

            return models;
        }

        public void MoveFile(string sPath, string dPath)
        {
            try
            {
                File.Copy(sPath, dPath, true);
                File.Delete(sPath);
                Console.WriteLine("Moved Success"); // Success
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex); // Write error
            }
        }
    }
}
