using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class Product
    {

        [Key]
        public int ProdID { get; set; }

        public string ProdName { get; set; }

        public int? CatID { get; set; }


        public virtual List<ProductFeatureValue> ProductFeatureValues { get; set; }

    }
}