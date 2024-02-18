using FruitShop.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitShop.Business.User
{
    public interface IUserBusiness
    {
        public Task<UserDTO> LoginAsync(string phone, string password);
        public Task<UserDTO> RegisterAsync(string phone, string password);
    }
}
