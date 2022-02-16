namespace WebApi.Models
{
    public class ProductModel
    {
        public ProductModel()
        {

        }

        public ProductModel(int id, string name, string description, decimal price, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }

        public ProductModel(int id, string name, string description, decimal price, string category, int categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            CategoryId = categoryId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
    }
}
