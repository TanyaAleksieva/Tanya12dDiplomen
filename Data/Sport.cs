namespace FleksTanya12d.Data
{
    public class Sport
    {
        public int Id { get; set; }
        public string SportName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
