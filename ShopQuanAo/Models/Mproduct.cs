namespace ShopQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class Mproduct
    {
        public int ID { get; set; }

        public int catid { get; set; }

        public int Submenu { get; set; }

        
        public string name { get; set; }

        
        [StringLength(255)]
        public string slug { get; set; }

       
        [StringLength(100)]
        public string img { get; set; }

        [Column(TypeName = "ntext")]

        public string detail { get; set; }

        public int number { get; set; }

        public double price { get; set; }

        public double pricesale { get; set; }

        [StringLength(150)]
        public string metakey { get; set; }

        public string metadesc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime updated_at { get; set; }

        public int updated_by { get; set; }

        public int status { get; set; }
        public int sold { get; set; }
    }
}
