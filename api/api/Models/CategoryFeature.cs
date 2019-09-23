using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class CategoryFeature
    {
        //[Key]
        //public int ID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int CatID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int FeatureID { get; set; }

        //public virtual Category Category { get; set; }

        //public virtual Feature Feature { get; set; }

    }
}