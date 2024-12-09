using LinqToDB.Mapping;

namespace HWLinq2DB.Domain
{
    [Table(Name = "customers")]
    internal class Customer
    {
        [PrimaryKey, Identity]
        [Column (Name = "id")]
        public int ID { get; set; }

        [Column(Name = "firstname")]
        public required string FirstName { get; set; }

        [Column(Name = "lastname")]
        public required string LastName { get; set; }

        [Column(Name = "age")]
        public required int Age { get; set; }
    }
}
