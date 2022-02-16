using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class CategoryEntity
    {
        public CategoryEntity()
        {
        }

        public CategoryEntity(string name)
        {
            Name = name;
        }

        public CategoryEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
