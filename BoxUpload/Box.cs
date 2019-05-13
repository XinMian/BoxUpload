using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Box.V2.Models;
using BoxUpload.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BoxUpload
{
    public class Box
    {
        public OAuthSession BoxAuthenicatedSession { get; set; }

        public Box()
        {

        }

        public async void JwtAuthen()
        {
            // Read in config file
            IBoxConfig config = null;
            using (FileStream fs = new FileStream($"D:\\iconextJWTconfig.json", FileMode.Open))
            {
                config = BoxConfig.CreateFromJsonFile(fs);
            }

            // Create JWT auth using config file
            var boxJWT = new BoxJWTAuth(config);


            // Create admin client
            var adminToken = boxJWT.AdminToken();
            var client = boxJWT.AdminClient(adminToken);

            // Set upload values
            List<MyFile> myFiles = new List<MyFile>();

            for (int i=1; i<=10; i++)
            {
                MyFile myFile = new MyFile();
                myFile.path = "D:\\CRM" + i + ".pdf";
                myFile.name = "CRM" + Guid.NewGuid().ToString("N") + i + ".pdf";

                myFiles.Add(myFile);
            }
           
            var folderId = "76164273241";


            foreach(var item in myFiles)
            {
                try
                {
                    string fileId = "";
                    BoxFile newFile;
                    using (FileStream stream = new FileStream(item.path, FileMode.Open))
                    {
                        BoxFileRequest req = new BoxFileRequest()
                        {
                            Name = item.name,
                            Parent = new BoxRequestEntity() { Id = folderId }
                        };

                        newFile = await client.FilesManager.UploadAsync(req, stream);
                        Console.Out.Write("File ID : " + newFile.Id);
                        fileId = newFile.Id;
                    }

                    if(string.IsNullOrEmpty(fileId))
                    {
                        continue;
                    }
                }
                catch(Exception e)
                {
                    Console.Out.Write("Error : " + e.Message);
                }
                //Stream fileContents = await client.FilesManager.DownloadStreamAsync(id: "455392071332");
            }
            // Upload file
        }

        private async void UploadFile(string path, string name, string folderId, BoxClient client)
        {
            string fileId = "";
            BoxFile newFile;
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                BoxFileRequest req = new BoxFileRequest()
                {
                    Name = name,
                    Parent = new BoxRequestEntity() { Id = folderId }
                };

                newFile = await client.FilesManager.UploadAsync(req, stream);
                Console.Out.Write("File ID : " + newFile.Id);
                fileId = newFile.Id;
            }
        }
        public class MyFile
        {
            public string name { get; set; }
            public string path { get; set; }
        }
    }
}
