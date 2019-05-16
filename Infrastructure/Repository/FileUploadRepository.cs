using ApplicationCore.Entities;
using ApplicationCore.Helper;
using ApplicationCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Infrastructure.Repository
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly DbContextOptionBuilder option;
        public FileUploadRepository(DbContextOptionBuilder option)
        {
            this.option = option;
        }

        public List<FileUpload> Gets()
        {
            using (var db = new BoxContext(option))
            {
                return db.FileUpload.ToList();
            }
            
        }

        public List<FileUpload> GetForUploads()
        {
            using (var db = new BoxContext(option))
            {
                return db.FileUpload
                    .Where(x => string.IsNullOrEmpty(x.DFileId) && string.IsNullOrEmpty(x.ErrorLog))
                    .ToList();
            }

        }

        public FileUpload Get(int id)
        {
            using (var db = new BoxContext(option))
            {
                return db.FileUpload.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Update(FileUpload item)
        {
            using (var db = new BoxContext(option))
            {
                var model = db.FileUpload.FirstOrDefault(x => x.Id == item.Id);
                model.DFileId = item.DFileId;
                model.ErrorLog = item.ErrorLog;

                db.SaveChanges();
            }
        }

        public void Insert(FileUpload item)
        {
            using (var db = new BoxContext(option))
            {
                db.FileUpload.Add(item);
                db.SaveChanges();
            }
        }

        public void Inserts(List<FileUpload> items)
        {
            using (var db = new BoxContext(option))
            {
                db.FileUpload.AddRange(items);
                db.SaveChanges();
            }
        }

        public int CountUnProcess()
        {
            int count = GetForUploads().Count;

            return count;
        }
    }
}
