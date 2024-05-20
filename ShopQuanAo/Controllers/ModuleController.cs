using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopQuanAo.Models;

namespace ShopQuanAo.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        ShopQuanAoDbContext db = new ShopQuanAoDbContext();
        public ActionResult Mainmenu()
        {
            var list = db.Menus.Where(m => m.status == 1).
                Where(m => m.parentid == 0 && m.position == "mainmenu")
                .OrderBy(m => m.orders);
            return View("_Mainmenu", list.ToList());
        }
        public ActionResult submainmenu(int parentid)
        {
            ViewBag.mainmenuitem = db.Menus.Find(parentid);
            var list = db.Menus.Where(m => m.status == 1).
                Where(m => m.parentid == parentid && m.position == "mainmenu")
                .OrderBy(m => m.orders);
            if (list.Count() != 0)
            {
                return View("~/Views/Shared/Submenu/_submainmenu1.cshtml", list);
            }
            else
            {
                return View("~/Views/Shared/Submenu/_submainmenu2.cshtml", list);
            }

        }

        public ActionResult Listcategory()
        {
            var list = db.Categorys.Where(m => m.status == 1).
               Where(m => m.parentid == 0)
               .OrderBy(m => m.orders);
            return View("_Listcategory", list.ToList());
        }
        public ActionResult SubListcategory(int parentid)
        {

            ViewBag.mainmenuitem = db.Categorys.Find(parentid);
            var list = db.Categorys.Where(m => m.status == 1).
                Where(m => m.parentid == parentid)
                .OrderBy(m => m.orders);
            if (list.Count() != 0)
            {
                return View("~/Views/Shared/SubCategory/_subcategory1.cshtml", list);
            }
            else
            {
                return View("~/Views/Shared/SubCategory/_subcategory2.cshtml", list);
            }

        }
        public ActionResult slider()
        {
            var list = db.Sliders
                .Where(m => m.status == 1 && m.position == "SliderShow").OrderBy(m => m.orders).ToList();
            return View("_Slider", list);
        }
        public ActionResult header()
        {
            if (Session["id"].Equals(""))
            {
                ViewBag.name = "";
            }
            else
            {
                ViewBag.name = Session["user"];
                ViewBag.id = int.Parse(Session["id"].ToString());
            }
            return View("_HeaderHome");
        }
    }
}