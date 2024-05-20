using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopQuanAo.Models;
using PagedList;
namespace ShopQuanAo.Controllers
{
    public class TrangchuController : Controller
    {
        // GET: Trangchu 
        ShopQuanAoDbContext db = new ShopQuanAoDbContext();
        public ActionResult Index()
        {
            var list = db.Categorys.Where(m => m.status == 1).
               Where(m => m.parentid == 0)
               .OrderBy(m => m.orders);
            return View(list);
        }
       
        public ActionResult productHome(int id)
        {
            var list = db.Products.Where(m => m.status == 1).
                Where(m => m.catid == id || m.Submenu == id).OrderBy(m=>m.ID).OrderBy(m=>m.ID).Take(8); 
            return View("~/Views/Trangchu/_productHome.cshtml", list);
        }
        public ActionResult productsale()
        {
            var list = db.Products.Where(m => m.status == 1).
                Where(m => m.pricesale>0).OrderBy(m => m.ID).OrderBy(m => m.ID).Take(8);
            return View("_ProductSale", list);
        }
        
    }
}