using ShopQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShopQuanAo.Controllers
{
    public class CheckoutController : BaseController
    {
        private const string SessionCart = "SessionCart";
        ShopQuanAoDbContext db = new ShopQuanAoDbContext();

        public ActionResult Index()
        {
            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
            }
            return View(list);
         
        }
        [HttpPost]
        public async Task<ActionResult> Index(Morder order)
        {
            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
            }
            Random rand = new Random((int)DateTime.Now.Ticks);
            int RandomNumber;
            RandomNumber = rand.Next(100, 100000);
            if (ModelState.IsValid)
            {
                
                order.code = RandomNumber.ToString();
                order.userid = 1;
                order.created_ate = DateTime.Now;
                order.updated_by = 1;
                order.updated_at = DateTime.Now;
                order.status = 2;
                order.exportdate = DateTime.Now;
                order.StatusPayment = 1;
                db.Orders.Add(order);
                db.SaveChanges();

                ViewBag.name = order.deliveryname;
                ViewBag.email = order.deliveryemail;
                ViewBag.address = order.deliveryaddress;
                ViewBag.code = order.code;
                ViewBag.phone = order.deliveryphone;
                Mordersdetail orderdetail = new Mordersdetail();
               
                foreach (var item in list)
                {
                    float price = 0;
                    int sale = (int)item.product.pricesale;
                    if (sale > 0)
                    {
                         price = (float)item.product.price - (int)item.product.price / 100 * (int)sale * item.quantity;
                    }
                    else {
                         price = (float)item.product.price * (int)item.quantity;
                    }
                    orderdetail.orderid = order.ID;
                    orderdetail.productid = item.product.ID;
                    orderdetail.priceSale = (int)item.product.pricesale;
                    orderdetail.price = item.product.price;
                    orderdetail.quantity = item.quantity;
                    orderdetail.amount = price;
                    
                    db.Orderdetails.Add(orderdetail);
                    await db.SaveChangesAsync();
                    //ViewBag.sump = list.Sum((Func<Cart_item, int>)(m => (int)m.product.price * (int) m.quantity));
                    // change number product         
                    var updatedProduct = db.Products.Find(item.product.ID);
                    updatedProduct.catid = item.product.catid;
                    updatedProduct.Submenu = item.product.Submenu;
                    updatedProduct.name = item.product.name;
                    updatedProduct.slug = item.product.slug;
                    updatedProduct.img = item.product.img;
                    updatedProduct.detail = item.product.detail;
                    updatedProduct.number = (int)updatedProduct.number - (int)item.quantity;
                    updatedProduct.price = item.product.price;
                    updatedProduct.pricesale = item.product.pricesale;
                    updatedProduct.metakey = item.product.metakey;
                    updatedProduct.metadesc = item.product.metadesc;
                    updatedProduct.created_by = item.product.created_by;
                    updatedProduct.created_at = item.product.created_at;
                    updatedProduct.updated_by = item.product.updated_by;
                    updatedProduct.updated_at = item.product.updated_at;
                    updatedProduct.status = item.product.status;
                    db.Products.Attach(updatedProduct);
                     db.Entry(updatedProduct).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                ViewBag.cart = (List<Cart_item>)cart;
                Session["SessionCart"] = null;
                var listProductOrder = db.Orderdetails.Where(m => m.orderid == order.ID);
                return View("payment", listProductOrder.ToList());
            }
            ViewBag.error = "Đặt hàng thất bại";
            return View("index");

        }
      

    }
}