using FruitShop.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitShop.Business.Order
{
    public interface IOrderBusiness
    {
        public Task<Guid> AddUpdateAsync(OrderDTO orderDTO);
    }
}
