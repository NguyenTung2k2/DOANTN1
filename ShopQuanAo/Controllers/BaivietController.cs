using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopQuanAo.Models;
using PagedList;
namespace ShopQuanAo.Controllers
{
    public class BaivietController : Controller

    {
        // GET: Post
        ShopQuanAoDbContext db = new ShopQuanAoDbContext();
        [ValidateInput(false)]
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.page = page;
            var list = db.posts.Where(m => m.status == 1).OrderByDescending(m => m.ID).OrderBy(m=>m.ID);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        [ValidateInput(false)]
        public ActionResult topic_category( String slug)
        {
            var catid = db.topics.Where(m => m.status == 1 && m.slug == slug).First();
            return View("_post_category",catid);
        }
        [ValidateInput(false)]
        public ActionResult post_category(int catid,int? page, string slug)
        {
            if (page == null) page = 1;
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            ViewBag.page = page;
            ViewBag.slug = slug;
            var list = db.posts.Where(m => m.status == 1 && m.topid == catid).OrderBy(m=>m.ID);
            return View("_post", list.ToPagedList(pageNumber, pageSize));
        }
        [ValidateInput(false)]
        public ActionResult topiccategory()
        {
            var list = db.topics.Where(m => m.status == 1).OrderBy(m => m.orders).ToList();
            return View("_topic_post",list);
        }
        [ValidateInput(false)]
        public ActionResult detail(String slug)
        {
            var detail = db.posts.Where(m => m.status == 1 && m.slug == slug).First();
            return View("_post_detail", detail);
        }
    }
}