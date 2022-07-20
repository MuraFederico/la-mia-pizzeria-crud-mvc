using la_mia_pizzeria_static.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("Pizza")]
    public class Pizza
    {
        public Pizza()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [CustomDescription]
        public string Description { get; set; }

        [Required]
        [Range(0, 20)]

        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public List<Ingredient>? Ingredients { get; set; }
    }
}
