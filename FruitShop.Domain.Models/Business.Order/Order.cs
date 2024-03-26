using FruitShop.Domain.Models.Business.Base;

namespace FruitShop.Domain.Models.Business.Order
{
    public class Order : MSSQLEntity
    {
        public Guid OrderID { get; set; }
        public Guid CustomerID { get; set; }
        
    }
}
