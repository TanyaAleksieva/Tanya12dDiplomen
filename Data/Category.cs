namespace FleksTanya12d.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        ICollection<Product> Products { get; set; }
    }
}
