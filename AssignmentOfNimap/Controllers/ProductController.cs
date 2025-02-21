using AssignmentOfNimap.Models;
using AssignmentOfNimap.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssignmentOfNimap.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService service;
        private readonly ICategoryService cat;
        public ProductController(IProductService service, ICategoryService cat)
        {
            this.service = service;
            this.cat = cat;
        }
        //public ActionResult Index()
        //{
        //    return View(service.GetProducts());
        //}

        public ActionResult Index(int pg = 1)
        {
            var p = service.GetProducts();
            const int pagesize = 9;
            if (pg < 1)
            {
                pg = 1;
            }

            int recscount = p.Count();

            var pager = new Pager(recscount, pg, pagesize);

            int recskip = (pg - 1) * pagesize;

            var data = p.Skip(recskip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
            return View(data);
        }

        public ActionResult Details(int id)
        {
            return View(service.GetProductById(id));
        }

        public ActionResult Create()
        {
            ViewBag.Categories = cat.GetCategories();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product prod, IFormFile file)
        {
            try
            {
                var result = service.AddProduct(prod);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Something went wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        //public ActionResult Edit(int id)
        //{
        //    return View(service.GetProductById(id));


        //}
        //public ActionResult Edit(int id)
        //{
        //    var product = service.GetProductById(id);

        //    var categories = cat.GetCategories(); 
        //    ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);

        //    return View(product);
        //}
        public ActionResult Edit(int id)
        {
            var product = service.GetProductById(id);
            ViewBag.Categories = new SelectList(cat.GetCategories(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Product prod)
        //{
        //    try
        //    {
        //        var result = service.UpdateProduct(prod);
        //        if (result >= 1)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            ViewBag.Error = "Error";
        //            return View();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //        return View();
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product prod)
        {
            try
            {
                var result = service.UpdateProduct(prod);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            // Ensure ViewBag.Categories is always set before returning the view
            ViewBag.Categories = new SelectList(cat.GetCategories(), "CategoryId", "CategoryName", prod.CategoryId);

            return View(prod);
        }


        public ActionResult Delete(int id)
        {
            return View(service.GetProductById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                var result = service.DeleteProduct(id);
                if (result >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error";
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
