using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopQuanAo.Models;
using PagedList;
namespace ShopQuanAo.Controllers
{
    public class SanphamController : Controller
    {
        // GET: Sanpham
        ShopQuanAoDbContext db = new ShopQuanAoDbContext();
        public ActionResult index(int? page)
        {
            var list = db.Products.Where(m => m.status == 1).OrderBy(m=>m.ID);
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.page = page;
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult category(String slug)
        {
            var catid = db.Categorys.Where(m =>m.slug == slug).First();
            return View(catid);
        }

        public ActionResult detail( String slug)
        {
            var list = db.Products.Where(m => m.status == 1 && m.slug == slug).First();
            return View(list);
        }
        public ActionResult cungloai(int catid)
        {
            var list = db.Products.Where(m => m.catid == catid && m.status == 1);
            return View("~/Views/Sanpham/_cungloai_detail.cshtml",list.ToList().Take(6));
        }
        public ActionResult subcategory(int catid,string slug, int? page)
        {
            var list = db.Products.Where(m => m.catid == catid || m.Submenu==catid && m.status == 1).OrderBy(m=>m.ID);
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.page = page;
            ViewBag.slug = slug;
            return View("~/Views/Sanpham/_Subcategory.cshtml", list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SearchProduct(string keyw, int? page) {
            @ViewBag.keyw = keyw;
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var list = db.Products.Where(m => m.status == 1 && m.name.Contains(keyw)).OrderBy(m => m.ID);
            return View("~/Views/Sanpham/_SearchProduct.cshtml", list.ToPagedList(pageNumber, pageSize));
        }

    }
}