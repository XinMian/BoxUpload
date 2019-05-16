using ApplicationCore.Entities;
using ApplicationCore.Helper;
using ApplicationCore.Service;
using Box.V2;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace Console
{
    class Program
    {
        private static readonly IConfigurationRoot configuration = Config.GetConfiguration("appsettings.json");
        private static MyFile myFile = new MyFile();

        static void Main(string[] args)
        {
            //Get Config
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var config = Config.GetConfig(configuration);

            //Get File From Folder
            List<FileUpload> files = myFile.GetFiles(config.SourceFolder, config.DestinationFolderId);

            // Db Context
            var option = new DbContextOptionBuilder(connectionString);
            FileUploadRepository fileUploadRepository = new FileUploadRepository(option);
            fileUploadRepository.Inserts(files);

            List<FileUpload> fileForUploads = fileUploadRepository.GetForUploads();
            MyBox box = new MyBox(fileUploadRepository);
            BoxClient client = box.JwtAuthen();
            box.UploadFileToBox(client, fileForUploads, config.SuccessFolder, config.ErrorFolder);

            System.Console.ReadLine();
        }
    }
}
