using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Shoping.Data_Access.DB.MongoDB
{
    public class MongoDBContext<TEntity> : DbContext where TEntity : class
    {
        public string _connectionString { get; set; }
        public string _databaseName { get; set; }

        public MongoDBContext(IConfiguration iConfiguration, string databaseName)
        {
            _connectionString = iConfiguration.GetSection("Database").GetSection("ConnectionString").Value;
            _databaseName = databaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMongoDB(_connectionString, _databaseName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TEntity>();
        }
    }
}
