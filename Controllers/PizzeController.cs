using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzeController : Controller
    {
        public IActionResult Index()
        {
            PizzaContext context = new PizzaContext();
            
            IQueryable<Pizza> pizzas = context.Pizzas;
            return View(pizzas);
            
        }

        [HttpGet]
        public IActionResult CreateForm()
        {
/*            PizzaContext context = new PizzaContext();
            List<Ingredient> ingredients = context.Ingredients.ToList();*/

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {

            if (!ModelState.IsValid)
            {
                return View("CreateForm", pizza);
            }

            using(PizzaContext context = new PizzaContext())
            {
                List<Ingredient> ingredients = context.Ingredients.ToList();
                pizza.Ingredients = ingredients;
                context.Add(pizza);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            PizzaContext context = new PizzaContext();
            Pizza pizza = context.Pizzas.Where(pizza => pizza.Id == id).Include("Ingredients").FirstOrDefault();
            return View(pizza);
        }
    }
}
