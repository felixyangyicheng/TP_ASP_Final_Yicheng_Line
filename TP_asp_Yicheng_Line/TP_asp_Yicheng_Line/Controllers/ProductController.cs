using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP_asp_Yicheng_Line.DB;
using TP_asp_Yicheng_Line.DB.Models;
using TP_asp_Yicheng_Line.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TP_asp_Yicheng_Line.Controllers
{
    public class ProductController : Controller
    {

        private string connectionString;

        public ProductController(IConfiguration configRoot)
        {
            connectionString = configRoot["ConnectionStrings:DefaultConnection"];

        }
       
        public IActionResult Index()
        {
            ProductContext productContext = new ProductContext(connectionString);
            List<Product> products = productContext.GetAll();

            ProductsViewModel model = new ProductsViewModel();
            model.Products = products;

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            ProductContext productContext = new ProductContext(connectionString);
            bool isOK = productContext.Delete(id);

            DeleteProductViewModel model = new DeleteProductViewModel();
            model.ProductIsDeleted = isOK;
            
            return View(model);

        }

        private List<SelectListItem> ListCategory()
        {
            CategoryContext categoryContext = new CategoryContext(connectionString);

            List<Category> categories = categoryContext.GetAll();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            foreach(Category category in categories)
            {
                selectListItem.Add(new SelectListItem(category.Libelle,  category.Identifiant.ToString() ));
            }

            return selectListItem;
        }


        public IActionResult Create()
        {

            ProductViewModel model = new ProductViewModel();
            
            model.Categories = ListCategory();


            return View(model);
        }

        [HttpPost]

        public  IActionResult Create (ProductViewModel productModel)
        {
            ProductContext productContext = new ProductContext(connectionString);

            productModel.Categories = ListCategory();
            if (productModel.IdentifiantCategory < 1)
            {

                ModelState.AddModelError("IdentifiantCategory", "Ne peut être inférieur à 1");
            }

            IActionResult retour = null;

            if (ModelState.IsValid)
            {

                Product product = new Product();

                product.Titre = productModel.Titre;
                product.Prix = productModel.Prix;
                product.IdentifiantCategory = productModel.IdentifiantCategory;

                bool isOK = productContext.Insert(product);
                retour = RedirectToAction("Index");
            }

            else
            {
                retour = View(productModel);
            }


            return retour;
        }

        public IActionResult Edit(int id)
        {
            ProductContext productContext = new ProductContext(connectionString);
            Product product = productContext.Get(id);
            ProductViewModel productModel = new ProductViewModel();

            productModel.Identifiant = product.Identifiant;
            productModel.Titre = product.Titre;
            productModel.Prix = product.Prix;
            productModel.IdentifiantCategory = product.IdentifiantCategory;

            productModel.Categories = ListCategory();



            return View(productModel);
        }

        [HttpPost]

        public IActionResult Edit(ProductViewModel productModel)
        {
            ProductContext productContext = new ProductContext(connectionString);
            productModel.Categories = ListCategory();

            IActionResult retour = null;

            if (ModelState.IsValid)
            {
                Product product = new Product();

                product.Identifiant = (int)productModel.Identifiant;
                product.Titre = productModel.Titre;
                product.Prix = productModel.Prix;
                product.IdentifiantCategory = productModel.IdentifiantCategory;

                bool isOK = productContext.Update(product);
                retour = RedirectToAction("Index");


            }

            else
            {
                retour = View(productModel);
            }



            return retour;
        }
    }
}
