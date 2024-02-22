using FruitShop.Domain.Models.Business.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitShop.Domain.Models.DTO
{
    public class UserDTO : BaseDTO
    {
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime TokenExpired { get; set; }
    }
}
