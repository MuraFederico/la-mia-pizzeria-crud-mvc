using la_mia_pizzeria_static.DB;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.RelationshipsModels;
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

            PizzaContext context = new PizzaContext();
            PizzaRelationships model = new PizzaRelationships();

            model.Categories = context.Categories.ToList();
            model.Ingredients = context.Ingredients.ToList();
            model.Pizza = new Pizza();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaRelationships model)
        {

            if (!ModelState.IsValid)
            {
                using (PizzaContext context = new PizzaContext())
                {
                    model.Categories = context.Categories.ToList();
                    return View("CreateForm", model);
                }
            }

            using(PizzaContext context = new PizzaContext())
            {
                List<Ingredient> ingredients = context.Ingredients.ToList();
                model.Pizza.Ingredients = ingredients;
                context.Add(model.Pizza);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            PizzaContext context = new PizzaContext();
            Pizza pizza = context.Pizzas.Where(pizza => pizza.Id == id).Include("Ingredients").Include("Category").FirstOrDefault();
            return View(pizza);
        }

        public IActionResult EditForm(int Id)
        {
            PizzaContext context = new PizzaContext();
            PizzaRelationships model = new PizzaRelationships();

            model.Categories = context.Categories.ToList();
            model.Pizza = context.Pizzas.Where(p => p.Id == Id).First();

            if(model.Pizza == null)
            {
                return NotFound();
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, PizzaRelationships model)
        {
            if (!ModelState.IsValid)
            {
                using (PizzaContext context = new PizzaContext())
                {
                    model.Categories = context.Categories.ToList();
                    return View("EditForm", model);
                }
            }
            using (PizzaContext context = new PizzaContext())
            {
                Pizza toEdit = context.Pizzas.Where(p => p.Id == Id).First();
                if (toEdit != null)
                {
                    toEdit.Name = model.Pizza.Name;
                    toEdit.Description = model.Pizza.Description;
                    toEdit.Price = model.Pizza.Price;
                    toEdit.ImageUrl = model.Pizza.ImageUrl;
                    toEdit.CategoryId = model.Pizza.CategoryId;
                    context.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            PizzaContext context = new PizzaContext();
            Pizza toDelete = context.Pizzas.Where(p => p.Id == Id).First();
            
            if(toDelete != null)
            {
                context.Pizzas.Remove(toDelete);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
