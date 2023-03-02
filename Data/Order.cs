namespace FleksTanya12d.Data
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }
        public string UserId { get; set; }
        public User Users { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderedOn { get; set; }
    }
}
