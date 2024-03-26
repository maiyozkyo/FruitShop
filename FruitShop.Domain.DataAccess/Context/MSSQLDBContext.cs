using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Core.Configuration;

namespace FruitShop.Domain.DataAccess.Context
{
    public class MSSQLDBContext<TEntity> : DbContext where TEntity : class
    {
        public string _connectionString {  get; set; }
        public string _dbName { get; set; }
        public MSSQLDBContext(IConfiguration iConfiguration, string dbName)
        {
            var dbSettings = iConfiguration.GetSection("Database");
            var server = dbSettings.GetSection("Server").Value;
            var user = dbSettings.GetSection("User").Value;
            var password = dbSettings.GetSection("Password").Value;
            _connectionString = $"Server={server};Database={dbName};User Id={user};Password={password};Trusted_Connection=True;TrustServerCertificate=True;";
            _dbName = dbName;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TEntity>();
        }
    }
}
