namespace WebAPI_HTTP.DBContext
{
    using Microsoft.EntityFrameworkCore;
    using WebAPI_HTTP.Models.DboModels;

    public class AddAppContext : DbContext
    {
        public DbSet<DboUser> DboUser { get; set; }
        public DbSet<DboPost> DboPost { get; set; }

        // Constructor accepting DbContextOptions
        public AddAppContext(DbContextOptions<AddAppContext> options)
            : base(options)
        {
        }

        public AddAppContext()
        {
        }

        /// <summary>
        /// Configure connection string
        /// </summary>
        /// <param name="dbContextOptionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            // This is optional if you're using the constructor to pass options
            // but can be useful for debugging or if you want to set default options.
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer("Server=OGN-LT31\\MSSQLSERVER03;Database=master;Trusted_Connection=True;TrustServerCertificate=Yes");
            }
        }

        /// <summary>
        /// Use to create indexes, relations, foreign keys
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
