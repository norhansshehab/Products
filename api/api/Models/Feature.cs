using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class Feature
    {
        [Key]
        public int FeatureID { get; set; }

        public string FeatureName { get; set; }

        public virtual List<CategoryFeature> CategoryFeatures { get; set; }

        public virtual List<ProductFeatureValue> ProductFeatureValues { get; set; }

    }
}