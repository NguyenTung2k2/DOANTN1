using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using ShopQuanAo.Common;
using ShopQuanAo.Models;

namespace ShopQuanAo.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(RoleID = "ADMIN")]
    public class OrdersController : BaseController
    {
        private ShopQuanAoDbContext db = new ShopQuanAoDbContext();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var list = db.Orders.Where(m => m.status != 0).ToList();
            return View(list);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.customer = db.Orders.Where(m => m.ID == id).First();
            var lisst = db.Orderdetails.Include(m => m.product).Where(m => m.orderid == id).ToList();
            return View("Orderdetail", lisst);
        }
        //status
        public ActionResult Status(int id)
        {
            Morder morder = db.Orders.Find(id);
            morder.status = (morder.status == 1) ? 2 : 1;
            morder.updated_at = DateTime.Now;
            morder.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(morder).State = EntityState.Modified;
            db.SaveChanges();
            _ = SendMailSendGrid(morder.deliveryemail, morder.status == 1 ? "ĐÃ DUYỆT" : "HỦY DUYỆT", morder.code);
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }
        //trash
        public ActionResult trash()
        {
            var list = db.Orders.Where(m => m.status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            Morder morder = db.Orders.Find(id);
            morder.status = 0;
            morder.updated_at = DateTime.Now;
            morder.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(morder).State = EntityState.Modified;
            db.SaveChanges();
            _ = SendMailSendGrid(morder.deliveryemail, "TỪ CHỐI", morder.code);
            Message.set_flash("Xóa thành công", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            Morder morder = db.Orders.Find(id);
            morder.status = 2;
            morder.updated_at = DateTime.Now;
            morder.updated_by = int.Parse(Session["Admin_id"].ToString());
            db.Entry(morder).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Khôi phục thành công", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Morder morder = db.Orders.Find(id);
            db.Orders.Remove(morder);
            db.SaveChanges();
            Message.set_flash("Đã xóa vĩnh viễn 1 Đơn hàng", "success");
            return RedirectToAction("trash");
        }

        private async Task<bool> SendMailSendGrid(string to, string notification, string code)
        {
            var result = false;
            var _client = new SendGridClient("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");
            var msg = MailHelper.CreateSingleTemplateEmail(
                new EmailAddress("meovipvip123@gmail.com", "THAY ĐỔI TRẠNG THÁI ĐƠN HÀNG"),
                new EmailAddress(to),
                "d-32de82436d704a809d7ff07536093796",
                new { notification  = notification, code = code  }
            );

            var response = await _client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);

            if (response.StatusCode.Equals(System.Net.HttpStatusCode.Accepted)
                || response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                result = true;
            }
            return result;
        }
    }
}
