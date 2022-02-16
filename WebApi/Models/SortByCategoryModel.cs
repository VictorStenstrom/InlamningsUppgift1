namespace WebApi.Models
{
    public class SortByCategoryModel
    {
        public SortByCategoryModel()
        {
        }

        public SortByCategoryModel(string name, string? description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
