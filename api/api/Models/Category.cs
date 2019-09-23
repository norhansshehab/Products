using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class Category
    {
        [Key]
        public int CatID { get; set; }

        public string CatName { get; set; }

        [ForeignKey("ParentCategory")]
        public int? ParentID { get; set; }
        public virtual Category ParentCategory { get; set; }

        public virtual List<CategoryFeature> CategoryFeatures { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}