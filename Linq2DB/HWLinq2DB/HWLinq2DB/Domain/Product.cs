using LinqToDB.Mapping;

namespace HWLinq2DB.Domain
{
    [Table(Name = "products")]
    internal class Product
    {
        [PrimaryKey, Identity]
        [Column(Name = "id")]
        public int ID { get; set; }

        [Column(Name = "name")]
        public required string Name { get; set; }

        [Column(Name = "description")]
        public required string Description { get; set; }

        [Column(Name = "stockquantity")]
        public required int StockQuantity { get; set; }

        [Column(Name = "price")]
        public required decimal Price { get; set;}
    }
}
