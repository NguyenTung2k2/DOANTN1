using ShopQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopQuanAo.Controllers
{
    public class FavoriteProductController : Controller
    {
        private const string SessionFavorite = "favorite";
        // GET: Cart
        ShopQuanAoDbContext db = new ShopQuanAoDbContext();
        // GET: FavoriteProduct

        public ActionResult favoriteList()
        {
            var favorite = Session[SessionFavorite];
            var list = new List<MfavoriteProduct>();
            if (favorite != null)
            {
                list = (List<MfavoriteProduct>)favorite;
            }
            return View("_listFavorite", list);
        }


        public JsonResult Additem(long productID)
        {
            var item = new MfavoriteProduct();
            Mproduct product = db.Products.Find(productID);
            var favorite = Session[SessionFavorite];
            if (favorite != null)
            {
                var list = (List<MfavoriteProduct>)favorite;
                if (list.Exists(m => m.favoriteProduct.ID == productID))
                {
                    return Json(new
                    {        
                        status = 1,
                        meThod = "ExistProduct"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    item.favoriteProduct = product;
                    item.status = 2;
                    list.Add(item);

                    item.method = "favoriteExist";
                    return Json(item, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                item.favoriteProduct = product;
                item.status = 3;
                item.method = "favoriteEmpty";
                var list = new List<MfavoriteProduct>();
                list.Add(item);
                Session[SessionFavorite] = list;

            }
            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}