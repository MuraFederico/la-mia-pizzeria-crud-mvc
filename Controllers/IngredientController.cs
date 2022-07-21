using la_mia_pizzeria_static.DB;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class IngredientController : Controller
    {
        // GET: IngredientController
        public ActionResult Index()
        {
            PizzaContext context = new PizzaContext();
 
            IQueryable<Ingredient> ingredients = context.Ingredients;
            return View(ingredients);
    
           
        }

        // GET: IngredientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IngredientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngredientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return View(ingredient);
            }

            using(PizzaContext context = new PizzaContext())
            {
                context.Add(ingredient);
                context.SaveChanges();
            }

            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientController/Edit/5
        public ActionResult Edit(int id)
        {
            using(PizzaContext context = new PizzaContext())
            {
                Ingredient ingredient = context.Ingredients.Where(i => i.Id == id).FirstOrDefault();
                return View(ingredient);
            }
        }

        // POST: IngredientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Ingredient edited)
        {
            if (!ModelState.IsValid)
            {
                return View(edited);
            }

            using(PizzaContext context = new PizzaContext())
            {
                Ingredient toEdit = context.Ingredients.Where(i => i.Id == id).FirstOrDefault();

                toEdit.Name = edited.Name;
                context.SaveChanges();
            }
                
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientController/Delete/5
        public ActionResult Delete(int Id)
        {
            PizzaContext context = new PizzaContext();
            Ingredient toDelete = context.Ingredients.Where(p => p.Id == Id).Include("Pizzas").First();

            if (toDelete != null)
            {
                context.Pizzas.RemoveRange(toDelete.Pizzas);
                context.Ingredients.Remove(toDelete);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }

        // POST: IngredientController/Delete/5
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }*/
}
