using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ShopQuanAo.Models
{
    public class ShopQuanAoDbContext : DbContext
    {
        public ShopQuanAoDbContext() : base("name=MyConnection")
        { }
        public virtual DbSet<Mcategory> Categorys { get; set; }
        public virtual DbSet<Mcontact> Contacts { get; set; }
        public virtual DbSet<Mmenu> Menus { get; set; }
        public virtual DbSet<Morder> Orders { get; set; }
        public virtual DbSet<Mordersdetail> Orderdetails { get; set; }
        public virtual DbSet<Mpost> posts { get; set; }
        public virtual DbSet<Mproduct> Products { get; set; }
        public virtual DbSet<Mslider> Sliders { get; set; }
        public virtual DbSet<Mtopic> topics { get; set; }
        public virtual DbSet<Muser> users { get; set; }
        public virtual DbSet<role> roles { get; set; }
        internal int ExecuteScalar()
        {
            throw new NotImplementedException();
        }
    }
}
