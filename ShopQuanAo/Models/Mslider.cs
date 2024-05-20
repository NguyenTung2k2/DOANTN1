namespace ShopQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("slider")]
    public partial class Mslider
    {
        public int ID { get; set; }

      
        [StringLength(255)]
        public string name { get; set; }

      
        [StringLength(255)]
        public string url { get; set; }

      
        [StringLength(100)]
        public string position { get; set; }

      
        [StringLength(100)]
        public string img { get; set; }

        public int? orders { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime created_at { get; set; }

        public int? created_by { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }
    }
}
