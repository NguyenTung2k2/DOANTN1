using ShopQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShopQuanAo.Library;
using System.Threading.Tasks;
using System.Net;

namespace ShopQuanAo.Controllers
{
    public class UserController : BaseController
    {

        ShopQuanAoDbContext db = new ShopQuanAoDbContext();
        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muser muser = db.users.Find(id);
            if (muser == null)
            {
                return HttpNotFound();
            }
            return View("index", muser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Muser muser)
        {
            if (ModelState.IsValid)
            {
                muser.img = "ádasd";
                muser.access = 1;
                muser.created_at = DateTime.Now;
                muser.updated_at = DateTime.Now;
                muser.created_by = int.Parse(Session["id"].ToString());
                muser.updated_by = int.Parse(Session["id"].ToString());
                db.Entry(muser).State = EntityState.Modified;
                db.SaveChanges();
                Message.set_flash("Cập nhật thành công", "success");
                return View("index", muser);
            }
            return View("index", muser);
        }

        public ActionResult ChangePassWord(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muser muser = db.users.Find(id);
            if (muser == null)
            {
                return HttpNotFound();
            }
            return View("_changePassword", muser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassWord(Muser muser ,FormCollection fc)
        {
            string oldPass = Mystring.ToMD5(    fc["passOld"]);
            string rePass = Mystring.ToMD5(fc["rePass"]);
            string newPass = Mystring.ToMD5(fc["password1"]);
            var pass_account = db.users.Where(m => m.password == oldPass).ToList().Count();
            if (pass_account==0)
            {
                    ViewBag.status = "Mật khẩu không đúng";
                    return View("_changePassword", muser);
            }
                else if (rePass != newPass)
                {
                    ViewBag.status = "2 Mật khẩu không khớp";
                    return View("_changePassword", muser);
                }
                else
                {
                if (ModelState.IsValid)
                {
                    var updatedPass = db.users.Find(muser.ID);

                    updatedPass.fullname = muser.fullname;
                    updatedPass.username = muser.username;
                    updatedPass.email = muser.email;
                    updatedPass.phone = muser.phone;
                    updatedPass.gender = muser.gender;
                    updatedPass.img = "bav";
                    updatedPass.password = newPass;
                    updatedPass.access = 1;
                    updatedPass.created_at = muser.created_at;
                    updatedPass.updated_at = DateTime.Now;
                    updatedPass.created_by = muser.created_by;
                    updatedPass.updated_by = int.Parse(Session["id"].ToString());
                    updatedPass.status = 1;

                    db.users.Attach(updatedPass);
                    db.Entry(updatedPass).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    Message.set_flash("Đổi mật khẩu thành công", "success");
                    return Redirect("~/tai-khoan/" + muser.ID + "");
                }
                }
                return View("_changePassword", muser);
            }

            
        }
    }