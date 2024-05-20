using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;

using System.Web;
using System.Web.Mvc;
using ShopQuanAo.Common;
using ShopQuanAo.Models;

namespace ShopQuanAo.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(RoleID = "SALESMAN")]
    public class ProductsController : BaseController
    {
        private ShopQuanAoDbContext db = new ShopQuanAoDbContext();

       
        // GET: Admin/Products
        public ActionResult Index()
        {
           
            var list = db.Products.Where(m => m.status != 0).OrderByDescending(m=>m.ID).ToList(); 
            return View(list);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            
            ViewBag.listCate = db.Categorys.Where(m => m.status != 0 && m.ID>2).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create( Mproduct mproduct , HttpPostedFileBase file)
        {
            ViewBag.listCate = db.Categorys.Where(m => m.status != 0 && m.ID > 2).ToList();
            if (ModelState.IsValid)
            {
                string slug = Mystring.ToSlug(mproduct.name.ToString());
                if (db.Categorys.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("Sản phẩm đã tồn tại trong bảng Category", "danger");
                    return View(mproduct);
                }
                if (db.topics.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash("Sản phẩm đã tồn tại trong bảng Topic", "danger");
                    return View(mproduct);
                }
                if (db.Products.Where(m => m.slug == slug).Count() > 0)
                {
                    Message.set_flash(" Sản phẩm đã tồn tại trong bảng Product", "danger");
                    return View(mproduct);
                }
                // lấy tên loại sản phẩm
                var namecateDb = db.Categorys.Where(m => m.ID == mproduct.catid).First();
                string namecate = Mystring.ToStringNospace(namecateDb.name);
                // lấy tên ảnh
                file = Request.Files["img"];
                string filename =  file.FileName.ToString();
                //lấy đuôi ảnh
                string ExtensionFile = Mystring.GetFileExtension(filename);
                // lấy tên sản phẩm làm slug
                
                //lấy tên mới của ảnh slug + [đuôi ảnh lấy đc]
                string namefilenew = namecate+"/"+slug + "." + ExtensionFile;
                //lưu ảnh vào đường đẫn
                var path = Path.Combine(Server.MapPath("~/public/images"), namefilenew);
                //nếu thư mục k tồn tại thì tạo thư mục
                var folder = Server.MapPath("~/public/images/"+ namecate);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                file.SaveAs(path);
                mproduct.img = namefilenew;
                mproduct.slug = slug;
                mproduct.sold = 0;
                mproduct.created_at = DateTime.Now;
                mproduct.updated_at = DateTime.Now;
                mproduct.created_by = int.Parse(Session["Admin_id"].ToString());
                mproduct.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Products.Add(mproduct);
                db.SaveChanges();
                Message.set_flash("Thêm thành công", "success");
                return RedirectToAction("index");
            }
            Message.set_flash("Thêm Thất Bại", "danger");
            return View(mproduct);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mproduct mproduct = db.Products.Find(id);
            if (mproduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.listCate = db.Categorys.Where(m => m.status != 0 && m.ID > 2).ToList();
            return View(mproduct);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit( Mproduct mproduct, HttpPostedFileBase file)
        {
           
            if (ModelState.IsValid)
            {
                string slug = Mystring.ToSlug(mproduct.name.ToString());
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                if (filename.Equals("") == false)
                {
                    var namecateDb = db.Categorys.Where(m => m.ID == mproduct.catid).First();
                    string namecate = Mystring.ToStringNospace(namecateDb.name);
                    string ExtensionFile = Mystring.GetFileExtension(filename);
                    string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                    var path = Path.Combine(Server.MapPath("~/public/images"), namefilenew);
                    var folder = Server.MapPath("~/public/images/" + namecate);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    file.SaveAs(path);
                    mproduct.img = namefilenew;
                }
                mproduct.slug = slug;
                mproduct.updated_at = DateTime.Now;
                mproduct.updated_by = int.Parse(Session["Admin_id"].ToString());
                db.Entry(mproduct).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.listCate = db.Categorys.Where(m => m.status != 0 && m.ID > 2).ToList();
                Message.set_flash("Sửa thành công", "success");
                return RedirectToAction("Index");
            }
            Message.set_flash("Sửa thất bại", "danger");
            ViewBag.listCate = db.Categorys.Where(m => m.status != 0 && m.ID > 2).ToList();
            return View(mproduct);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mproduct mproduct = db.Products.Find(id);
            if (mproduct == null)
            {
                return HttpNotFound();
            }
            return View(mproduct);
        }
        public ActionResult Status(int id)
        {
            Mproduct mproduct = db.Products.Find(id);
            mproduct.status = (mproduct.status == 1) ? 2 : 1;
            mproduct.updated_at = DateTime.Now;
            mproduct.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mproduct).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }
        public ActionResult trash()
        {
            var list = db.Products.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            Mproduct mproduct = db.Products.Find(id);
            mproduct.status = 0;
            mproduct.updated_at = DateTime.Now;
            mproduct.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mproduct).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Retrash(int id)
        {
            Mproduct mproduct = db.Products.Find(id);
            mproduct.status = 2;
            mproduct.updated_at = DateTime.Now;
            mproduct.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mproduct).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("khôi phục thành công", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Mproduct mproduct = db.Products.Find(id);
            db.Products.Remove(mproduct);
            db.SaveChanges();
            Message.set_flash("Đã xóa vĩnh viễn 1 sản phẩm", "success");
            return RedirectToAction("trash");
        }

    }
}
