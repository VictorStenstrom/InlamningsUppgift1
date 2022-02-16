using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class ProductEntity
    {
        public ProductEntity()
        {
        }

        public ProductEntity(string name, decimal price, int categoryId)
        {
            Name = name;
            Price = price;
            CategoryId = categoryId;
        }

        public ProductEntity(string name, string description, decimal price, int categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
}
