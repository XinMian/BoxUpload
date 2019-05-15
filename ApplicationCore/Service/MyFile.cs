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
                File.Move(sPath, sPath); // Try to move
                Console.WriteLine("Moved"); // Success
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex); // Write error
            }
        }
    }
}
