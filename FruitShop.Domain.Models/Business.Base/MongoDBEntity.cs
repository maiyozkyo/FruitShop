using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FruitShop.Domain.Models.Business.Base
{
    public class MongoDBEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [Key]
        public string ID { get; set; }

        public Guid RecID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public int Index { get; set; }

        public MongoDBEntity()
        {
            RecID = Guid.NewGuid();
            ID = RecID.ToString();
            CreatedOn = DateTime.Now;
        }
    }
}
