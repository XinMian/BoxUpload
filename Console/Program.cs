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
        private static readonly IConfigurationRoot configuration = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)).Build();
        static void Main(string[] args)
        {
            //Get Config
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var mySettingsConfig = new MySettingsConfig();
            configuration.GetSection("MySettings").Bind(mySettingsConfig);

            MyFile myFile = new MyFile();
            var files = myFile.GetFiles(mySettingsConfig.SourceFolder);
            List<FileUpload> fileUploads = myFile.ToFileUpload(files, mySettingsConfig.DestinationFolderId);

            // Db Context
            var option = new DbContextOptionBuilder(connectionString);
            FileUploadRepository fileUploadRepository = new FileUploadRepository(option);
            fileUploadRepository.Inserts(fileUploads);

            List<FileUpload> fileForUploads = fileUploadRepository.GetForUploads();

            MyBox box = new MyBox(fileUploadRepository);
            BoxClient client = box.JwtAuthen();
            box.UploadFileToBox(client, fileForUploads, mySettingsConfig.SuccessFolder, mySettingsConfig.ErrorFolder);

            System.Console.ReadLine();
        }
    }
}
