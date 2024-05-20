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
    public class ContactController : BaseController
    {
        private ShopQuanAoDbContext db = new ShopQuanAoDbContext();

        // GET: Admin/Contact
   
        public ActionResult Index()
        {
            var list = db.Contacts.Where(m => m.status > 0).ToList();
            return View(list);
        }

        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mcontact mcontact = db.Contacts.Find(id);
            if (mcontact == null)
            {
                return HttpNotFound();
            }
            return View(mcontact);
        }

  
        public ActionResult Status(int id)
        {
            Mcontact mcontact = db.Contacts.Find(id);
            mcontact.status = (mcontact.status == 1) ? 2 : 1;
            mcontact.updated_at = DateTime.Now;
            mcontact.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mcontact).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }
        //trash
        public ActionResult trash()
        {
            var list = db.Contacts.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            Mcontact mcontact = db.Contacts.Find(id);
            mcontact.status = 0;
            mcontact.updated_at = DateTime.Now;
            mcontact.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mcontact).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            Mcontact mcontact = db.Contacts.Find(id);
            mcontact.status = 2;
            mcontact.updated_at = DateTime.Now;
            mcontact.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(mcontact).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("khôi phục thành công", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Mcontact mcontact = db.Contacts.Find(id);
            db.Contacts.Remove(mcontact);
            db.SaveChanges();
            Message.set_flash("Đã xóa vĩnh viễn 1 Liên Hệ", "success");
            return RedirectToAction("trash");
        }

    }
}
