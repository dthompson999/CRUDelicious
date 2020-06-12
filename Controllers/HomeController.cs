using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = dbContext.Dishes
                                    .OrderByDescending(d => d.CreatedAt)
                                    .ToList();
                                    return View(AllDishes);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("process")]
        public IActionResult Process(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Add");
            }
            
        }

        [HttpGet("dish/{DishId}")]
        public IActionResult Show(int DishId)
        {
            Dish dish = dbContext.Dishes.FirstOrDefault(d => d.DishId == DishId);
            return View(dish);
        }

        [HttpGet("dish/{DishId}/edit")]
        public IActionResult Edit(int DishId)
        {
            Dish dish = dbContext.Dishes.FirstOrDefault(d => d.DishId == DishId);
            return View(dish);
        }

        [HttpPost("dish/{DishId}/update")]
        public IActionResult Update(int DishId, Dish dUpdated)
        {
            Dish dish = dbContext.Dishes.FirstOrDefault(d => d.DishId == DishId);
            dish.Name = dUpdated.Name;
            dish.Chef = dUpdated.Chef;
            dish.Tastiness = dUpdated.Tastiness;
            dish.Calories = dUpdated.Calories;
            dish.Description = dUpdated.Description;
            dish.DataImageUrlField = dUpdated.DataImageUrlField;
            dish.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return View("Show", dish);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
