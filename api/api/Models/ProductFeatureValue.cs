using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class ProductFeatureValue
    {
        [Key]
        [Column(Order = 1)]
        public int ProdID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int FeatureID { get; set; }
        [Key]
        [Column(Order = 3)]
        public string Value { get; set; }
    }
}