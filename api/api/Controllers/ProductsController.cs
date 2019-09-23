using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using api.Models;

namespace api.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private MyDbContext db = new MyDbContext();

        

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(Product new_product)
        {
            Product existingProduct = db.Products.Where(c => c.ProdID == new_product.ProdID).Include(cf => cf.ProductFeatureValues).SingleOrDefault();

            if (existingProduct != null)
            {
                // Update Product
                db.Entry(existingProduct).CurrentValues.SetValues(new_product);

                // Delete ProductFeatureValues
                foreach (var existingPFV in existingProduct.ProductFeatureValues.ToList())
                {
                    if (!new_product.ProductFeatureValues.Any(cf => cf.ProdID == existingPFV.ProdID && cf.FeatureID == existingPFV.FeatureID && cf.Value == existingPFV.Value))
                        db.ProductFeatureValues.Remove(existingPFV);
                }

                // Update and Insert ProductFeatureValues
                foreach (var nPFV in new_product.ProductFeatureValues)
                {
                    var existingProductFeatureValues = existingProduct.ProductFeatureValues.Where(cf => cf.ProdID == nPFV.ProdID && cf.FeatureID == nPFV.FeatureID && cf.Value == nPFV.Value).SingleOrDefault();

                    if (existingProductFeatureValues != null)
                        // Update ProductFeatureValues
                        db.Entry(existingProductFeatureValues).CurrentValues.SetValues(nPFV);

                    else
                    {
                        // Insert ProductFeatureValues
                        var newProductFeatureValue = new ProductFeatureValue
                        {
                            ProdID = nPFV.ProdID,
                            FeatureID = nPFV.FeatureID,
                            Value = nPFV.Value
                            //...
                        };
                        existingProduct.ProductFeatureValues.Add(nPFV);
                    }
                }
                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ProdID }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        [Route("GetFeaturesForSelectedCategory/{id}")]
        [HttpGet]
        public IHttpActionResult GetFeaturesForSelectedCategory(int id)
        {
            List<Feature> features = db.Features.Where(f => f.CategoryFeatures.Any(cf => cf.CatID == id)).Select(f => f).ToList();
            if (features == null)
            {
                return NotFound();
            }

            return Ok(features);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProdID == id) > 0;
        }
    }
}