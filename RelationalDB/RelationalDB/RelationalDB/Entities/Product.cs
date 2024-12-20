namespace RelationalDB.Entities
{
    internal class Product
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
    }
}
