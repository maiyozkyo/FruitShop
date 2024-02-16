
using FruitShop.Domain.Models.Business.Base;

namespace Shoping.Data_Access.Models
{
    public class User : MongoDBEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

    }
}
