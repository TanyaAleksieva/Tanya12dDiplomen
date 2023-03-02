namespace FleksTanya12d.Data
{
    public class Type
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
