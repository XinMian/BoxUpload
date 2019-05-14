using ApplicationCore.Entities;
using ApplicationCore.Helper;
using ApplicationCore.Service;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
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

            string sourceFolder = mySettingsConfig.SourceFolder;
            string errorFolder = mySettingsConfig.ErrorFolder;


            var files = MyFile.GetFiles(mySettingsConfig.SourceFolder);

            // Db Context
            var option = new DbContextOptionBuilder(connectionString);
            FileUploadRepository fileUploadRepository = new FileUploadRepository(option);
            var fileUploads = fileUploadRepository.Gets();


            System.Console.ReadLine();
        }
    }
}
