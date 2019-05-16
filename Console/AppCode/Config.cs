using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Console
{
    public static class Config
    {
        public static IConfigurationRoot GetConfiguration(string configName)
        {
            IConfigurationRoot configuration = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(configName, optional: true, reloadOnChange: true)).Build();

            return configuration;
        }

        public static ConfigModel GetConfig(IConfigurationRoot configuration)
        {
            var model = new ConfigModel();
            configuration.GetSection("MySettings").Bind(model);

            return model;
        }
    }
}
