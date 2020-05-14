using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using TP_asp_Yicheng_Line.DB;
using TP_asp_Yicheng_Line.DB.Models;
using TP_asp_Yicheng_Line.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TP_asp_Yicheng_Line.Controllers
{
    public class CategoryController : Controller
    {
        // GET: /<controller>/

        private string connectionString;

        public CategoryController(IConfiguration configRoot)
        {
            connectionString = configRoot["ConnectionStrings:DefaultConnection"];

        }

        public IActionResult Index()
        {
            CategoryContext categoryContext = new CategoryContext(connectionString);
            List<Category> categories = categoryContext.GetAll();

            CategoriesViewModel model = new CategoriesViewModel();
            model.Categories = categories;

            return View(model);
        }

        public IActionResult Delete(int id) 
        {
            CategoryContext categoryContext = new CategoryContext(connectionString);
            bool isOK = categoryContext.Delete(id);

            DeleteCategoryViewModel model = new DeleteCategoryViewModel();
            model.CategoryIsDeleted = isOK;

            return View(model);
        }

        private IActionResult Create()
        {
            CategoryViewModel model = new CategoryViewModel();

            return View(model);
        }

        [HttpPost]

        public IActionResult Create(CategoryViewModel categoryModel)
        {
            CategoryContext categoryContext = new CategoryContext(connectionString);

            IActionResult retour = null;
            if (ModelState.IsValid)
            {
                Category category = new Category();

                category.Libelle = categoryModel.Libelle;
                category.Date = (DateTime)categoryModel.Date;

                bool isOK = categoryContext.Insert(category);
                retour = RedirectToAction("Index");
            }
            else
            {
                retour = View(categoryModel);
            }

            return retour;

            
        }



        public IActionResult Edit(int id)
        {
            CategoryContext categoryContext = new CategoryContext(connectionString);
            Category category = categoryContext.Get(id);
            CategoryViewModel categoryModel = new CategoryViewModel();

            categoryModel.Identifiant = category.Identifiant;
            categoryModel.Libelle = category.Libelle;
            categoryModel.Date = category.Date;

            return View(categoryModel);
        }

        [HttpPost]

        public IActionResult Edit(CategoryViewModel categoryModel)
        {
            CategoryContext categoryContext = new CategoryContext(connectionString);
            IActionResult retour = null;

            if (ModelState.IsValid)
            {
                Category category = new Category();

                category.Identifiant = (int)categoryModel.Identifiant;
                category.Libelle = categoryModel.Libelle;
                category.Date = (DateTime)categoryModel.Date;

                bool isOK = categoryContext.Update(category);
                retour = RedirectToAction("Index");
            }
            else
            {
                retour = View(categoryModel);

            }
            return retour;
        }

    }
}
