namespace ShopQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("order")]
    public partial class Morder
    {
        public int ID { get; set; }

        public string code { get; set; }

        public int userid { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime created_ate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? exportdate { get; set; }

      
        [StringLength(255)]
        public string deliveryaddress { get; set; }

      
        [StringLength(100)]
        public string deliveryname { get; set; }

      
        [StringLength(255)]
        public string deliveryphone { get; set; }

      
        [StringLength(255)]
        public string deliveryemail { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }
        public string deliveryPaymentMethod { get; set; }
        public int StatusPayment { get; set; }
    }
}
