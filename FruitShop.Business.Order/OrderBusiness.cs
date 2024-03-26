using FruitShop.Domain.Models.DTO;
using Microsoft.Extensions.Configuration;
using Shoping.Business;

namespace FruitShop.Business.Order
{
    public class OrderBusiness : BaseBusiness<FruitShop.Domain.Models.Business.Order.Order>, IOrderBusiness
    {
        public OrderBusiness(IConfiguration iConfiguration) : base(iConfiguration)
        {
        }

        public async Task<Guid> AddUpdateAsync(OrderDTO orderDTO)
        {
            var order = await Repository.GetOneAsync(x => x.RecID == orderDTO.RecID);
            //Add
            if (order == null)
            {
                order = new Domain.Models.Business.Order.Order
                {
                    OrderID = Guid.NewGuid(),
                    CustomerID = orderDTO.CustomerID,
                };
                Repository.Add(order);
            }
            //Update
            else
            {
                order.CustomerID = orderDTO.CustomerID;
                order.ModifiedBy = "";
                order.ModifiedOn = DateTime.Now;
                Repository.Update(order);
            }
            await UnitOfWork.SaveChangesAsync();
            return order.OrderID;
        }
    }
}
