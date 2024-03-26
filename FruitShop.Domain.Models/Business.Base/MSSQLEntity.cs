using Microsoft.EntityFrameworkCore;

namespace FruitShop.Domain.Models.Business.Base
{
    [PrimaryKey(nameof(RecID))]
    public class MSSQLEntity
    {
        public Guid RecID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set;}

        public MSSQLEntity()
        {
            RecID = Guid.NewGuid();
            CreatedBy = "";
            CreatedOn = DateTime.Now;
            ModifiedBy = "";
            ModifiedOn = null;
        }
    }
}
