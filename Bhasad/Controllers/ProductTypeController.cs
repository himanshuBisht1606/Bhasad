using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bhasad.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly ProductTypeBAL ProductTypeBAL;
        public ProductTypeController()
        {
            ProductTypeBAL = ProductTypeBAL ?? new ProductTypeBAL();
        }
        // GET: ProductType
        public ActionResult Index()
        {
            var productType = ProductTypeBAL.GetProductType();
            return View(productType);
        }

        public ActionResult Add()
        {
            ProductTypeModel productType = new ProductTypeModel();
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductTypeModel model)
        {
            if (ModelState.IsValid)
            {
                ProductTypeBAL productTypeBAL = new ProductTypeBAL();
                var result = productTypeBAL.AddProductType(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int productTypeId)
        {
            ProductTypeModel productTypeModel = new ProductTypeModel();
            productTypeModel = ProductTypeBAL.GetProductTypeByProductTypeId(productTypeId);
            return View(productTypeModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductTypeModel productTypeModel)
        {
            if (ModelState.IsValid)
            {
                int result = ProductTypeBAL.UpdateProductType(productTypeModel);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int productTypeId)
        {
            int result = ProductTypeBAL.DeleteProductType(productTypeId);
            return RedirectToAction("Index");
        }
    }
}