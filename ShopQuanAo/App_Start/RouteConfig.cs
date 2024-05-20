using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopQuanAo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "loai bai viet",
            url: "chi-tiet-bai-viet/{slug}",
            defaults: new { controller = "Baiviet", action = "detail", id = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "them sp yeu thich",
            url: "addFavorite",
            defaults: new { controller = "FavoriteProduct", action = "Additem", id = UrlParameter.Optional },
            new[] { "ShopQuanAo.Controllers" }
            );

            routes.MapRoute(
           name: "send mail to user",
           url: "send-mail-to-user",
           defaults: new { controller = "Auth", action = "sendMail", id = UrlParameter.Optional },
           new[] { "ShopQuanAo.Controllers" }
           );

            routes.MapRoute(
           name: "quen mat khau",
           url: "quen-mat-khau",
           defaults: new { controller = "Auth", action = "forgetpass", id = UrlParameter.Optional },
           new[] { "ShopQuanAo.Controllers" }
           );


            routes.MapRoute(
            name: "xoa gio hang",
            url: "xoa-gio-hang",
            defaults: new { controller = "Cart", action = "deleteitem", id = UrlParameter.Optional },
            new[] { "ShopQuanAo.Controllers" }
            );

            routes.MapRoute(
        name: "them vao gio hang",
        url: "them-sp-giohang",
        defaults: new { controller = "Cart", action = "Additem", id = UrlParameter.Optional },
        new[] { "ShopQuanAo.Controllers" }
        );
            routes.MapRoute(
          name: "gio hang",
          url: "gio-hang",
          defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
         new[] { "ShopQuanAo.Controllers" }
        );
            routes.MapRoute(
             name: "Thanh toán",
             url: "Thanh-toan",
             defaults: new { controller = "checkout", action = "index", id = UrlParameter.Optional },
        new[] { "ShopQuanAo.Controllers" }
       );
            routes.MapRoute(
             name: "bai viet",
             url: "bai-viet",
             defaults: new { controller = "Baiviet", action = "Index", id = UrlParameter.Optional },
            new[] { "ShopQuanAo.Controllers" }
         );

            routes.MapRoute(
             name: "chi tiet bai viet",
             url: "bai-viet/{slug}",
             defaults: new { controller = "Baiviet", action = "topic_category", id = UrlParameter.Optional }
         );

            routes.MapRoute(
              name: "Lien He",
              url: "lien-he",
              defaults: new { controller = "Lienhe", action = "Index", id = UrlParameter.Optional }
          );
            
                 routes.MapRoute(
             name: "mat khau moi",
             url: "newPass/{id}",
             defaults: new { controller = "Auth", action = "newPasswordFG", id = UrlParameter.Optional },
              new[] { "ShopQuanAo.Controllers" }
         );
            routes.MapRoute(
             name: "doi mat khau",
             url: "doi-mat-khau/{id}",
             defaults: new { controller = "User", action = "ChangePassWord", id = UrlParameter.Optional },
            new[] { "ShopQuanAo.Controllers" }
         );

            routes.MapRoute(
              name: "Tai Khoang",
              url: "tai-khoan/{id}",
              defaults: new { controller = "User", action = "Edit", id = UrlParameter.Optional },
             new[] { "ShopQuanAo.Controllers" }
          );
            routes.MapRoute(
          name: "search",
          url: "sanpham/search",
          defaults: new { controller = "Sanpham", action = "SearchProduct", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "san pham",
            url: "sanpham",
            defaults: new { controller = "Sanpham", action = "index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
        name: "loai san pham",
        url: "sanpham/loaiSP/{slug}",
        defaults: new { controller = "Sanpham", action = "category", id = UrlParameter.Optional }
      );
            routes.MapRoute(
            name: "chi tiet san pham",
            url: "sanpham/chitiet-sp/{slug}",
            defaults: new { controller = "Sanpham", action = "detail", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "logout",
             url: "logout",
             defaults: new { controller = "Auth", action = "logout", id = UrlParameter.Optional }, new[] { "ShopQuanAo.Controllers" }
         );
            routes.MapRoute(
              name: "login",
              url: "login",
              defaults: new { controller = "Auth", action = "login", id = UrlParameter.Optional }, new[] { "ShopQuanAo.Controllers" }
          );
            routes.MapRoute(
             name: "register",
             url: "register",
             defaults: new { controller = "Auth", action = "register", id = UrlParameter.Optional }, new[] { "ShopQuanAo.Controllers" }
         );
            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "TrangChu", action = "index", id = UrlParameter.Optional }
          );
        }
    }
}
