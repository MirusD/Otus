using LinqToDB.Mapping;

namespace HWLinq2DB.Domain
{
    [Table(Name = "orders")]
    internal class Order
    {
        [PrimaryKey, Identity]
        [Column(Name = "id")]
        public int ID { get; set; }

        [Column(Name = "customerid")]
        public required int CustomerId { get; set; }

        [Association(ThisKey = "customerid", OtherKey = "id")]
        public required Customer Customer { get; set; }

        [Column(Name = "productid")]
        public required int ProductId { get; set; }

        [Association(ThisKey = "productid", OtherKey = "id")]
        public required Product Product { get; set; }

        [Column(Name = "quantity")]
        public required int Quantity { get; set; }
    }
}
