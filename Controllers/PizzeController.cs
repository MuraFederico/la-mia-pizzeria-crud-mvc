using la_mia_pizzeria_static.DB;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.RelationshipsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            model.Ingredients = new List<SelectListItem>();

            List<Ingredient> ingredients = context.Ingredients.ToList();

            foreach (Ingredient ingredient in ingredients)
            {
                model.Ingredients.Add(new SelectListItem { Text = ingredient.Name, Value = ingredient.Id.ToString() });
            }

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
                    List<Ingredient> ingredients = context.Ingredients.ToList();

                    foreach (Ingredient ingredient in ingredients)
                    {
                        model.Ingredients.Add(new SelectListItem { Text = ingredient.Name, Value = ingredient.Id.ToString() });
                    }

                    return View("CreateForm", model);
                }
            }

            using(PizzaContext context = new PizzaContext())
            {
                if(model.SelectedIngredients != null)
                {
                    model.Pizza.Ingredients = new List<Ingredient>();
                    foreach (string ingredientId in model.SelectedIngredients)
                    {
                        Ingredient ing = context.Ingredients.Where(i => i.Id == int.Parse(ingredientId)).FirstOrDefault();
                        model.Pizza.Ingredients.Add(ing);
                    }
                }
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

            model.Pizza = context.Pizzas.Where(p => p.Id == Id).Include("Ingredients").First();

            if(model.Pizza == null)
            {
                return NotFound();
            }
            else
            {
                model.Categories = context.Categories.ToList();
                model.Ingredients = new List<SelectListItem>();

                List<Ingredient> ingredients = context.Ingredients.ToList();

                foreach (Ingredient ingredient in ingredients)
                {
                    model.Ingredients.Add(new SelectListItem { Text = ingredient.Name, Value = ingredient.Id.ToString() });
                }

                model.SelectedIngredients = new List<string>();
                foreach (Ingredient ing in model.Pizza.Ingredients)
                {
                    model.SelectedIngredients.Add(ing.Id.ToString());
                }

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
                    List<Ingredient> ingredients = context.Ingredients.ToList();

                    foreach (Ingredient ingredient in ingredients)
                    {
                        model.Ingredients.Add(new SelectListItem { Text = ingredient.Name, Value = ingredient.Id.ToString() });
                    }
                    return View("EditForm", model);
                }
            }
            using (PizzaContext context = new PizzaContext())
            {
                Pizza toEdit = context.Pizzas.Where(p => p.Id == Id).Include("Ingredients").First();
                if (toEdit != null)
                {
                    toEdit.Name = model.Pizza.Name;
                    toEdit.Description = model.Pizza.Description;
                    toEdit.Price = model.Pizza.Price;
                    toEdit.ImageUrl = model.Pizza.ImageUrl;
                    toEdit.CategoryId = model.Pizza.CategoryId;
                    toEdit.Ingredients.Clear();
                    if (model.SelectedIngredients != null)
                    {
                        toEdit.Ingredients = new List<Ingredient>();
                        foreach (string ingredientId in model.SelectedIngredients)
                        {
                            Ingredient ing = context.Ingredients.Where(i => i.Id == int.Parse(ingredientId)).FirstOrDefault();
                            toEdit.Ingredients.Add(ing);
                        }
                    }
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
