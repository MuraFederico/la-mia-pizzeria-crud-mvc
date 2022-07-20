using la_mia_pizzeria_static.Models;

namespace la_mia_pizzeria_static.RelationshipsModels
{
    public class PizzaRelationships
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }

        public PizzaRelationships()
        {

        }
    }
}
