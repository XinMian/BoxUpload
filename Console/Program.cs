using ApplicationCore.Helper;
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
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var mySettingsConfig = new MySettingsConfig();
            configuration.GetSection("MySettings").Bind(mySettingsConfig);

            // Db Context
            var option = new DbContextOptionBuilder(connectionString);

            FileUploadRepository fileUploadRepository = new FileUploadRepository(option);
            var fileUploads = fileUploadRepository.Gets();

            foreach(var item in fileUploads)
            {
                System.Console.WriteLine("File Name : " + item.SPath);
            }

            System.Console.WriteLine("Hello World!");
            System.Console.ReadLine();
        }
    }
}
