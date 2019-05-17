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
            System.Console.WriteLine("Current Directory : " + Directory.GetCurrentDirectory());
            //Get Config
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var config = Config.GetConfig(configuration);

            //Get File From Folder
            List<FileUpload> files = myFile.GetFiles(config.SourceFolder, config.DestinationFolderId);
            FileUploadService fileService = new FileUploadService(files);

            // Db Context
            //var option = new DbContextOptionBuilder(connectionString);
            //FileUploadRepository fileUploadRepository = new FileUploadRepository(option);
            //fileUploadRepository.Inserts(files);

            //List<FileUpload> fileForUploads = fileUploadRepository.GetForUploads();
            MyBox box = new MyBox();
            BoxClient client = box.JwtAuthen();
            box.UploadFileToBox(client, files, config.SuccessFolder, config.ErrorFolder);


            int count = 99;
            while(count > 0)
            {
                System.Threading.Thread.Sleep(1000);
                count = fileService.countUnSuccessFile();
            }
        }
    }
}
