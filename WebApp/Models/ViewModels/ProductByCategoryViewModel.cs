namespace WebApp.Models.ViewModels
{
    public class ProductByCategoryViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
    }
}
