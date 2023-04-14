using System.ComponentModel.DataAnnotations.Schema;

namespace FleksTanya12d.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public Category Categories { get; set; }   

        public int TypeId { get; set; }
        public Type Types { get; set; }

        public int SportId { get; set; }
        public Sport Sports { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public string Image { get; set; }
        DateTime DateRegister { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        ///
       // public DateTime OrderedOn { get; internal set; }
    }
}
