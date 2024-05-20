namespace ShopQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("post")]
    public partial class Mpost
    {
        public int ID { get; set; }

        public int? topid { get; set; }

       
        public string title { get; set; }

       
        [StringLength(255)]
        public string slug { get; set; }

     
        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        [StringLength(255)]
        public string img { get; set; }

        [StringLength(50)]
        public string type { get; set; }

       
        [StringLength(150)]
        public string metakey { get; set; }

       
        [StringLength(150)]
        public string metadesc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime updated_at { get; set; }

        public int updated_by { get; set; }

        public int status { get; set; }
    }
}
