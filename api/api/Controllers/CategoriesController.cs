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
    public class CategoriesController : ApiController
    {
        private MyDbContext db = new MyDbContext();

        // GET: api/Categories
        public IQueryable<Category> GetCategories()
        {
            return db.Categories;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, Category new_category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (id != new_category.CatID)
            {
                return BadRequest();
            }

            var existingCategory = db.Categories.Where(c => c.CatID == new_category.CatID).Include(cf => cf.CategoryFeatures).SingleOrDefault();
            if (existingCategory != null)
            {
                // Update Category
                db.Entry(existingCategory).CurrentValues.SetValues(new_category);
                // Delete CategoryFeatures
                foreach (var existingCatFeature in existingCategory.CategoryFeatures.ToList())
                {
                    if (!new_category.CategoryFeatures.Any(cf => cf.FeatureID == existingCatFeature.FeatureID))
                        db.CategoryFeatures.Remove(existingCatFeature);
                }
                // Update and Insert CategoryFeatures
                foreach (var nCatFeat in new_category.CategoryFeatures)
                {
                    var existingCategoryFeature = existingCategory.CategoryFeatures.Where(cf => cf.FeatureID == nCatFeat.FeatureID).SingleOrDefault();
                    if (existingCategoryFeature != null) { 
                        nCatFeat.CatID = existingCategory.CatID;
                        // Update catFeature
                        db.Entry(existingCategoryFeature).CurrentValues.SetValues(nCatFeat);
                    }
                    else
                    {
                        // Insert CatFeature
                        var newCatFeature = new CategoryFeature
                        {
                            FeatureID = nCatFeat.FeatureID
                            //...
                        };
                        existingCategory.CategoryFeatures.Add(newCatFeature);
                    }
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
            
        }

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.CatID }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Where(c => c.CatID == id).Include(cf => cf.CategoryFeatures).Include(cp => cp.Products).SingleOrDefault();

            foreach (var existingCatFeature in category.CategoryFeatures.ToList())
            {
                if (!category.CategoryFeatures.Any(cf => cf.FeatureID == existingCatFeature.FeatureID))
                    db.CategoryFeatures.Remove(existingCatFeature);
            }
            foreach (var existingCatFeature in category.Products.ToList())
            {
                if (!category.Products.Any(cf => cf.CatID == existingCatFeature.CatID))
                    db.Products.Remove(existingCatFeature);
            }
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.CatID == id) > 0;
        }
    }
}