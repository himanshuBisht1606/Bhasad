using System.Web.Mvc;
using BusinessLayer;
using BusinessLayer.Models;

namespace Bhasad.Controllers
{
    public class TestProductController : Controller
    {
        // GET: Product
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TestProduct product)
        {
            TestProductBAL productBAL = new TestProductBAL();
            var entryInfo = productBAL.GetEntryInfo(product);
            int result = productBAL.SaveProduct(product);
            return View();
        }
            

        
    }
}