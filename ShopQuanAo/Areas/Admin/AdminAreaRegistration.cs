using System.Web.Mvc;

namespace ShopQuanAo.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
            "Admin_menu",
            "Admin/menu",
            new { Controller = "menu", action = "index", id = UrlParameter.Optional }
        );
            context.MapRoute(
            "Admin_user",
            "Admin/user",
            new { Controller = "user", action = "index", id = UrlParameter.Optional }
      );
            context.MapRoute(
            "Admin_orders",
            "Admin/orders",
            new { Controller = "orders", action = "index", id = UrlParameter.Optional }
        );
            context.MapRoute(
            "Admin_post",
            "admin/post",
            new { Controller = "post", action = "index", id = UrlParameter.Optional }
        );
            context.MapRoute(
            "Admin_topic",
            "Admin/topic",
            new { Controller = "topic", action = "index", id = UrlParameter.Optional }
        );
            context.MapRoute(
        "Admin_slider",
        "Admin/Slider",
        new { Controller = "Slider", action = "index", id = UrlParameter.Optional }
    );
            context.MapRoute(
            "Admin_category",
            "Admin/category",
            new { Controller = "Category", action = "Index", id = UrlParameter.Optional }
        );
            context.MapRoute(
            "Admin_contact",
            "Admin/contact",
            new { Controller = "contact", action = "index", id = UrlParameter.Optional }
        );
            context.MapRoute(
              "Admin_product",
              "Admin/product",
              new { Controller = "Products", action = "index", id = UrlParameter.Optional }
          );
            context.MapRoute(
           "Admin_clint_edit",
           "admin/info/{id}",
           new { Controller = "auth", action = "Edit", id = UrlParameter.Optional }
     );
            context.MapRoute(
               "Admin_infor",
               "Admin/info",
               new { Controller = "auth", action = "infor", id = UrlParameter.Optional }
           );
            context.MapRoute(
                "Admin_login",
                "Admin/login",
                new { Controller = "auth", action = "login", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_logout",
                "Admin/logout",
                new { Controller = "auth", action = "logout", id = UrlParameter.Optional }
            );
            
                 context.MapRoute(
                "statistics",
                "Admin/statistics",
                new { Controller = "Dashboard", action = "statistics", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}