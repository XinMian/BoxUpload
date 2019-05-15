using ApplicationCore.Entities;
using ApplicationCore.Repository;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace BoxUpload
{
    public class Box
    {
        public IFileUploadRepository fileUploadRepository { get; set; }

        public Box(IFileUploadRepository fileUploadRepository)
        {
            this.fileUploadRepository = fileUploadRepository;
        }

        public async void JwtAuthen(List<FileUpload> fileUploads)
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

            foreach(var item in fileUploads)
            {
                try
                {
                    BoxFile newFile;
                    using (FileStream stream = new FileStream(item.SPath, FileMode.Open))
                    {
                        BoxFileRequest req = new BoxFileRequest()
                        {
                            Name = item.DName,
                            Parent = new BoxRequestEntity() { Id = item.DFolderId }
                        };

                        newFile = await client.FilesManager.UploadAsync(req, stream);
                        Console.Out.Write("File ID : " + newFile.Id);
                        item.DFileId = newFile.Id;
                    }
                }
                catch(Exception e)
                {
                    item.ErrorLog = e.Message;
                }

                fileUploadRepository.Update(item);
            }
        }
    }
}
