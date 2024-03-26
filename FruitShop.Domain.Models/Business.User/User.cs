
using FruitShop.Domain.Models.Business.Base;

namespace FruitShop.Domain.Models.Business.User
{
    public class User : MongoDBEntity
    {
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsTrial { get; set; }
    }
}
