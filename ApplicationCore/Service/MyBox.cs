using ApplicationCore.Entities;
using ApplicationCore.Repository;
using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApplicationCore.Service
{
    public class MyBox
    {
        public IFileUploadRepository fileUploadRepository { get; set; }

        public MyBox(IFileUploadRepository fileUploadRepository)
        {
            this.fileUploadRepository = fileUploadRepository;
        }

        public BoxClient JwtAuthen()
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

            return client;

        }

        public async void UploadFileToBox(BoxClient client, List<FileUpload> fileUploads, string successPath, string errorPath)
        {
            MyFile myFile = new MyFile();
            foreach (var item in fileUploads)
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
                        Console.Out.WriteLine("Upload Success File ID : " + newFile.Id);
                        item.DFileId = newFile.Id;
                    }
                    myFile.MoveFile(item.SPath, successPath + item.DName);
                }
                catch (Exception e)
                {
                    item.ErrorLog = e.Message;
                    myFile.MoveFile(item.SPath, errorPath + item.DName);
                }

                fileUploadRepository.Update(item);
            }
        }
    }
}
