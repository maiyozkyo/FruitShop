using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shoping.Data_Access.DB.MongoDB;
using Shoping.Data_Access.DB.UnitOfWork;
using Shoping.Data_Access.Repo;

namespace Shoping.Business
{
    public class BaseBusiness<TEntity> where TEntity : class
    {
        public UnitOfWork<TEntity> UnitOfWork { get; set; }
        public Repository<TEntity> Repository { get; set; }

        public BaseBusiness(/*string _dbName, */IConfiguration iConfiguration)
        {
            var dbSection = iConfiguration.GetSection("Database");
            var type = dbSection?.GetSection("DBType").Value;
            var _dbName = dbSection?.GetSection("DBName").Value;
            DbContext dbContext = null;
            switch (type)
            {
                case "MongoDB":
                    {
                        dbContext = new MongoDBContext<TEntity>(iConfiguration, _dbName);
                        break;
                    }
                case "Posgrest":
                    {
                        break;
                    }
                case "MySQL":
                    {

                        break;
                    }
                default:
                {
                    throw new Exception($"Không có loại DataBase {type}");
                }
            }
            UnitOfWork = new UnitOfWork<TEntity>(dbContext);
            Repository = UnitOfWork.Repository;
        }

    }
}
