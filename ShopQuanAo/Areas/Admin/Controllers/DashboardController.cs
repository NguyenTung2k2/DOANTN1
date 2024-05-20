    using ShopQuanAo.Common;
using ShopQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopQuanAo.Areas.Admin.Controllers
{

    public class DashboardController : BaseController
    {
        private ShopQuanAoDbContext db = new ShopQuanAoDbContext();
        // GET: Admin/Dashboard
       
        public ActionResult Index()
        {

            ViewBag.product = db.Products.Count();
            ViewBag.Neworder = db.Orders.Where(m => m.status == 2).Count();
            ViewBag.Approvedorder = db.Orders.Where(m => m.status == 1).Count();
            ViewBag.Rejectorder = db.Orders.Where(m => m.status == 0).Count();
            ViewBag.contact = db.Contacts.Where(m => m.status == 0).Count();
            ViewBag.user = db.users.Where(m=> m.status ==1 && m.access==1).Count();
            ViewBag.category = db.Categorys.Count();
            ViewBag.post = db.posts.Count();
            ViewBag.topic = db.topics.Count();
            ViewBag.slider = db.Sliders.Count();

          
            //
            return View();
        }
        public ActionResult CallSessionForLayout()
        {
            ViewBag.adminName = Session["Admin_user"];
            ViewBag.adminID = int.Parse(Session["Admin_id"].ToString());
            ViewBag.adminFull = Session["Admin_fullname"];
            return View("_userNav");
        }

        [CustomAuthorizeAttribute(RoleID = "ACCOUNTANT")]
        public ActionResult statistics()
        {
            //order today
            DateTime dateNow = DateTime.Now;
            string shortDate = dateNow.ToString("yyyy-MM-dd");
            var Order = db.Orders;
            ViewBag.OrderToday = 0;
            foreach (var item in Order)
            {
                DateTime shortItem = Convert.ToDateTime(item.exportdate);
                string shortItem1 = shortItem.ToString("yyyy-MM-dd");
                if (shortItem1 == shortDate)
                {
                    ViewBag.OrderToday += 1;
                }
            }

            //order weed
            ViewBag.OrderWeek = 0;
            foreach (var item in Order)
            {
                DateTime shortItem = Convert.ToDateTime(item.exportdate);
                string shortItem1 = shortItem.ToString("yyyy-MM-dd");
                int d = (int)dateNow.Day;
                int m = (int)dateNow.Month;
                int y = (int)dateNow.Year;
                for (int i = 0; i < 7; i++)
                {
                    int day = d - i;
                    if (day <= 0)
                    {
                        --m;
                    }
                    if (m <= 0)
                    {
                        --y;
                    }
                    string shortWeek = "" + y + "-0" + m + "-0" + day + "";
                    if (shortItem1 == shortWeek)
                    {
                        ViewBag.OrderWeek += 1;
                    }

                }
              
            }
            return View("_Statistical");
        }
        public string CallSessionFullname()
        {
            //ViewBag.admiUser = Session["Admin_user"];
            string userFullname = Session["Admin_fullname"].ToString();
            return userFullname;
        }

    }
}