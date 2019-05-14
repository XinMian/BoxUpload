
using ApplicationCore.Helper;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Console
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        static void Main(string[] args)
        {
            // Db Context
            var connectionString = "Server=localhost;Database=Box;user id=sa;password=P@ssw0rd";
            var option = new DbContextOptionBuilder(connectionString);

            FileUploadRepository fileUploadRepository = new FileUploadRepository(option);
            var xx = fileUploadRepository.Gets();


            System.Console.WriteLine("Hello World!");
            System.Console.ReadLine();
        }
    }
}
