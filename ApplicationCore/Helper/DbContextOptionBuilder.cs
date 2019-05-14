using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Helper
{
    public class DbContextOptionBuilder
    {
        private readonly DbContextOptionsBuilder<BoxContext> builder;

        public DbContextOptionBuilder(string connectionStrings)
        {
            builder = new DbContextOptionsBuilder<BoxContext>();
            builder.UseSqlServer(connectionStrings);
        }

        internal DbContextOptions Build()
        {
            return builder.Options;
        }
    }
}
