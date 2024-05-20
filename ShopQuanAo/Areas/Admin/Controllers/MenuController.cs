using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopQuanAo.Common;
using ShopQuanAo.Models;

namespace ShopQuanAo.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(RoleID = "ADMIN")]
    public class MenuController : BaseController
    {
        private ShopQuanAoDbContext db = new ShopQuanAoDbContext();

        // GET: Admin/Menu
        public ActionResult Index()
        {
            ViewBag.listCate = db.Categorys.Where(m => m.status == 1).ToList();
            ViewBag.listTopic = db.topics.Where(m => m.status == 1).ToList();
            ViewBag.listPage = db.posts.Where(m => m.status == 1 && m.type == "page").ToList();
            var list = db.Menus.Where(m => m.status > 0).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Index(FormCollection data)
        {

            if (!string.IsNullOrEmpty(data["ADDPAGE"]))
            {
                
                var itemcatt = data["itempage"];
                if (itemcatt==null) { Message.set_flash("Bạn chưa chọn Mà", "danger");
                    return RedirectToAction("index");
                }
                var arrcat = itemcatt.Split(',');
                foreach (var rcat in arrcat)
                {
                    int id = int.Parse(rcat);
                    Mpost post = db.posts.Find(id);
                    Mmenu menu = new Mmenu();
                    menu.name = post.title;
                    menu.link = post.slug;
                    menu.position = data["position"];
                    menu.type = "menu";
                    menu.tableid = 2;
                    menu.parentid = 0;
                    menu.orders = 1;
                    menu.created_at = DateTime.Now;
                    menu.updated_at = DateTime.Now;
                    menu.created_by = int.Parse(Session["Admin_id"].ToString());
                    menu.updated_by = int.Parse(Session["Admin_id"].ToString());
                    menu.status = 1;
                    db.Menus.Add(menu);
                    db.SaveChanges();
                    Message.set_flash("Thêm thành công", "success");
                }
             
            }

            if (!string.IsNullOrEmpty(data["THEMCATE"]))
            {
                var itemcatt = data["itemCat"];
                if (itemcatt == null)
                {
                    Message.set_flash("Bạn chưa chọn Mà", "danger");
                    return RedirectToAction("index");
                }
                var arrcat = itemcatt.Split(',');
                foreach (var rcat in arrcat)
                {
                    int id = int.Parse(rcat);
                    Mcategory mcategory = db.Categorys.Find(id);
                    Mmenu menu = new Mmenu();
                    menu.name = mcategory.name;
                    menu.link = "loaiSP/"+mcategory.slug;
                    menu.position = data["position"];
                    menu.type = "menu";
                    menu.tableid = 2;
                    menu.parentid = 0;
                    menu.orders = 1;
                    menu.created_at = DateTime.Now;
                    menu.updated_at = DateTime.Now;
                    menu.created_by = int.Parse(Session["Admin_id"].ToString());
                    menu.updated_by = int.Parse(Session["Admin_id"].ToString());
                    menu.status = 1;
                    db.Menus.Add(menu);
                    db.SaveChanges();
                    Message.set_flash("Thêm thành công", "success");
                }
               
            }
            if (!string.IsNullOrEmpty(data["THEMTOPIC"]))
            {
                var itemcatt = data["itemtopic"];
                if (itemcatt == null)
                {
                    Message.set_flash("Bạn chưa chọn", "danger");
                    return RedirectToAction("index");
                }
                var arrcat = itemcatt.Split(',');
                foreach (var rcat in arrcat)
                {
                    int id = int.Parse(rcat);
                    Mtopic mtopic = db.topics.Find(id);
                    Mmenu menu = new Mmenu();
                    menu.name = mtopic.name;
                    menu.link = mtopic.slug;
                    menu.position = data["position"];
                    menu.type = "menu";
                    menu.tableid = 2;
                    menu.parentid = 0;
                    menu.orders = 1;
                    menu.created_at = DateTime.Now;
                    menu.updated_at = DateTime.Now;
                    menu.created_by = int.Parse(Session["Admin_id"].ToString());
                    menu.updated_by = int.Parse(Session["Admin_id"].ToString());
                    menu.status = 1;
                    db.Menus.Add(menu);
                    db.SaveChanges();
                    Message.set_flash("Thêm thành công", "success");
                }
              
            }
            if (!string.IsNullOrEmpty(data["THEMCUSS"]))
            {
                Mmenu menu = new Mmenu();
                menu.position = data["position"];
                menu.name = data["name"];
                menu.link = data["link"];
                menu.type = "menu";
                menu.tableid = 2;
                menu.parentid = 0;
                menu.orders = 1;
                menu.created_at = DateTime.Now;
                menu.updated_at = DateTime.Now;
                menu.created_by = int.Parse(Session["Admin_id"].ToString());
                menu.updated_by = int.Parse(Session["Admin_id"].ToString());
                menu.status = 1;
                db.Menus.Add(menu);
                db.SaveChanges();
                Message.set_flash("Thêm thành công", "success");
            }
            ViewBag.listCate = db.Categorys.Where(m => m.status == 1).ToList();
            ViewBag.listTopic = db.topics.Where(m => m.status == 1).ToList();
            ViewBag.listPage = db.posts.Where(m => m.status == 1 && m.type == "post").ToList();
            var list = db.Menus.Where(m => m.status > 0).ToList();

            return View(list);
        }

        // GET: Admin/Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mmenu mmenu = db.Menus.Find(id);
            if (mmenu == null)
            {
                return HttpNotFound();
            }
            return View(mmenu);
        }

        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mmenu mmenu = db.Menus.Find(id);
            if (mmenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.listMenu = db.Menus.Where(m => m.status != 0).ToList();
            return View(mmenu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,type,link,tableid,parentid,orders,position,created_at,created_by,updated_at,updated_by,status")] Mmenu mmenu)
        {
            if (ModelState.IsValid)
            {
                mmenu.updated_at = DateTime.Now;
                mmenu.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Entry(mmenu).State = EntityState.Modified;
                db.SaveChanges();
                Message.set_flash("Chỉnh sửa thành công", "success");
                return RedirectToAction("Index");
            }
            return View(mmenu);
        }
        public ActionResult Status(int id)
        {
            Mmenu mmenu = db.Menus.Find(id);
            mmenu.status = (mmenu.status == 1) ? 2 : 1;
            mmenu.updated_at = DateTime.Now;
            mmenu.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mmenu).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }
        //trash
        public ActionResult trash()
        {
            var list = db.Menus.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            Mmenu mmenu = db.Menus.Find(id);
            mmenu.status = 0;
            mmenu.updated_at = DateTime.Now;
            mmenu.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mmenu).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            Mmenu mmenu = db.Menus.Find(id);
            mmenu.status = 2;
            mmenu.updated_at = DateTime.Now;
            mmenu.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mmenu).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("khôi phục thành công", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Mmenu mmenu = db.Menus.Find(id);
            db.Menus.Remove(mmenu);
            db.SaveChanges();
            Message.set_flash("Đã xóa vĩnh viễn 1 Menu", "success");
            return RedirectToAction("trash");
        }
    }
}
