using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApplicationCore.Entities
{
    public partial class BoxContext : DbContext
    {
        public BoxContext()
        {
        }

        public BoxContext(DbContextOptions<BoxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FileUpload> FileUpload { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=Box;user id=sa;password=P@ssw0rd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<FileUpload>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DFileId)
                    .HasColumnName("dFileId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DFolderId)
                    .HasColumnName("dFolderId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DName)
                    .HasColumnName("dName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorLog)
                    .HasColumnName("errorLog")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SPath)
                    .HasColumnName("sPath")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
