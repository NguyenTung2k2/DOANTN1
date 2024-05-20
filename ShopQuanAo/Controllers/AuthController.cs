using ShopQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using ShopQuanAo.Library;

namespace ShopQuanAo.Controllers
{
    public class AuthController : Controller
    {
        ShopQuanAoDbContext db = new ShopQuanAoDbContext();
        public void login(FormCollection fc)
        {
            string Username = fc["uname"];
            string Pass = Mystring.ToMD5(fc["psw"]);
            string PassNoMD5 = fc["psw"];
            var user_account = db.users.Where(m => (m.username == Username) && (m.access == 1));

            if (user_account.Count() == 0)
            {
                Message.set_flash("Tên đăng nhập không tồn tại", "error");
            }
            else
            {
                var pass_account = db.users.Where(m => m.status == 1 && (m.password == Pass ) && (m.access == 1));

                if (pass_account.Count() == 0)
                {
                    Message.set_flash("Mật khẩu không đúng", "error");
                }

                else
                {
                    var user = user_account.First();
                    Session["id"] = user.ID;
                    Session["user"] = user.username;
                    ViewBag.name = Session["user"];
                    if (!Response.IsRequestBeingRedirected)
                        Message.set_flash("Đăng nhập thành công", "success");
                        Response.Redirect("/Trangchu/index");
                }
            }
            if (!Response.IsRequestBeingRedirected)
                Response.Redirect("/Trangchu/index");
        }
        public void logout()
        {
            Session["id"] = "";
            Session["user"] = "";
            Response.Redirect("/Trangchu/index");
            Message.set_flash("Đăng xuất thành công", "success");
        }
        public void register(Muser muser, FormCollection fc)
        {
            string uname = fc["uname"];
            string fname = fc["fname"];
            string Pass = Mystring.ToMD5(fc["psw"]);
            string email = fc["email"];
            string phone = fc["phone"];
            if (ModelState.IsValid)
            {
                var Luser = db.users.Where(m => m.status == 1 && m.username == uname && m.access == 1);
                if (Luser.Count() > 0)
                {
                    Message.set_flash("Tên Đăng nhập đã tồn tại", "success");
                    Response.Redirect("/Trangchu/index");
                }
                else
                {
                    muser.img = "defalt.png";
                    muser.password = Pass;
                    muser.username = uname;
                    muser.fullname = fname;
                    muser.email = email;
                    muser.phone = phone;
                    muser.gender = "nam";
                    muser.access = 1;
                    muser.created_at = DateTime.Now;
                    muser.updated_at = DateTime.Now;
                    muser.created_by = 1;
                    muser.updated_by = 1;
                    muser.status = 1;
                    db.users.Add(muser);
                    db.SaveChanges();
                    Message.set_flash("Tạo user  thành công", "success");
                    Response.Redirect("/Trangchu/index");
                }
            }
        }
        public ActionResult forgetpass()
        {
            return View();
        }

        public ActionResult newPasswordFG(int? id)
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
            return View("_newPasswordFG", muser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> newPasswordFG(Muser muser, FormCollection fc)
        {
            string rePass = Mystring.ToMD5(fc["rePass"]);
            string newPass = Mystring.ToMD5(fc["password1"]);
            if (rePass != newPass)
            {
                ViewBag.status = "2 Mật khẩu không khớp";
                return View("_newPasswordFG", muser);
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
                    updatedPass.updated_by = muser.ID;
                    updatedPass.status = 1;
                    db.users.Attach(updatedPass);
                    db.Entry(updatedPass).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    Message.set_flash("Reset Mật Khẩu thành công", "success");
                    return Redirect("~/Trangchu/index");
                }
            }
            ViewBag.status = "Vui lòng thử lại";
            return View("_newPasswordFG", muser);
        }
        public ActionResult sendMail()
        {
            //var username = Request.QueryString["username"];
            ViewBag.mess = "";
            var username = Request.Form["username"];
            var list = db.users.Where(m => m.access == 1 && m.status == 1 && m.username == username).Count();
            if (list <= 0)
            {
                ViewBag.mess = "Tên Đăng Nhập Không Đúng";
                return View("forgetPass");
            }
            else
            {
                var item = db.users.Where(m => m.access == 1 && m.status == 1 && m.username == username).First();
                // email gửi đi và email nhận
                MailMessage mm = new MailMessage(Util.email, item.email);
                mm.Subject = "Cấp Lại Mật khẩu Shop Quần Áo";
                mm.Body = "Dear Mr." + item.fullname + "," +
                    "Chúng tôi đã nhận được yêu cầu reset đổi mật khẩu của bạn, vui lòng tạo mới mật khẩu qua đường dẫn sau : http://localhost:22222/newPass/" + item.ID + "";
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                /// Email dùng để gửi đi
                NetworkCredential nc = new NetworkCredential(Util.email, Util.password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Send(mm);
                ViewBag.mess = item.email;
                return View("sendMailFinish");
            }
        }

    }
}