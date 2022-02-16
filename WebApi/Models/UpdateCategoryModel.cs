namespace WebApi.Models
{
    public class UpdateCategoryModel
    {
        public UpdateCategoryModel()
        {
        }

        public UpdateCategoryModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
