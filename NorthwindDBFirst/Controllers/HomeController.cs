using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindDBFirst.Models;
using System.Diagnostics;

namespace NorthwindDBFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        NorthwindContext db = new NorthwindContext();
        public IActionResult Index()
        {
            var list = db.Categories.ToList();
            return View(list);
        }

        public IActionResult _CategoryList()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Category category,int id)
        {
            var newCategory = db.Categories.Find(id);
            newCategory.CategoryName = category.CategoryName;
            newCategory.Description = category.Description;
            newCategory.CategoryId = id;
            db.SaveChanges();
            TempData["alertMessage"] = $"{category.CategoryName} Updated Successfuly";
            return RedirectToAction("Index");
        }

        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryVM categoryVM)
        {
            Category category = new Category();
            category.CategoryName = categoryVM.CategoryName;
            category.Description = categoryVM.Description;
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["alertMessage"] = $"{category.CategoryName} Deleted Successfuly";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}