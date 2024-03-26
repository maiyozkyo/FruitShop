using FruitShop.Domain.Models.Business.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitShop.Domain.Models.DTO
{
    public class OrderDTO : BaseDTO
    {
        public Guid OrderID { get; set; }
        public Guid CustomerID { get; set; }
    }
}
