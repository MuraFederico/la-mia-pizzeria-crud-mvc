using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.RelationshipsModels
{
    public class PizzaRelationships
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? Ingredients { get; set; }

        public List<string>? SelectedIngredients { get; set; }

        public PizzaRelationships()
        {

        }
    }
}
