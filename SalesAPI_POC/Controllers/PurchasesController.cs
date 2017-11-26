using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SalesAPI_POC.Models;

namespace SalesAPI_POC.Controllers
{
    public class PurchasesController : ApiController
    {
        private SalesAPI_POCContext db = new SalesAPI_POCContext();

        [HttpGet]
        [Route("api/purchases/TotalPurchase")]
        public async Task<IHttpActionResult> GetTotalPurchase()
        {
            var query = from p in db.Products
                        select new
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            TotalSalesCount = p.Purchase.Count(),
                            TotalSalesAmount = p.Purchase.Count() * p.Price,
                            CreatedDate = p.CreatedDate,
                            ModifiedDate = p.ModifiedDate
                        };

            return Ok(await query.ToListAsync());
        }

        [HttpGet]
        [Route("api/purchases/TotalPurchase")]
        public async Task<IHttpActionResult> GetTotalPurchase(int? productId)
        {
            var product = await db.Products.FindAsync(productId);

            if (product == null)
                return NotFound();

            var query = from p in db.Products
                        where p.Id == productId
                        select new
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            TotalSalesCount = p.Purchase.Count(),
                            TotalSalesAmount = p.Purchase.Count() * p.Price,                            
                            CreatedDate = p.CreatedDate,
                            ModifiedDate = p.ModifiedDate
                        };

            return Ok(await query.FirstOrDefaultAsync());
        }
        // GET: api/Purchases
        public IQueryable<Purchase> GetPurchases()
        {
            return db.Purchases;
        }

        // GET: api/Purchases/5
        [ResponseType(typeof(Purchase))]
        public async Task<IHttpActionResult> GetPurchase(int id)
        {
            Purchase purchase = await db.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        // PUT: api/Purchases/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPurchase(int id, Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.Id)
            {
                return BadRequest();
            }

            db.Entry(purchase).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/Purchases
        [ResponseType(typeof(Purchase))]
        public async Task<IHttpActionResult> PostPurchase(Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Purchases.Add(purchase);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = purchase.Id }, purchase);
        }

        // DELETE: api/Purchases/5
        [ResponseType(typeof(Purchase))]
        public async Task<IHttpActionResult> DeletePurchase(int id)
        {
            Purchase purchase = await db.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            db.Purchases.Remove(purchase);
            await db.SaveChangesAsync();

            return Ok(purchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseExists(int id)
        {
            return db.Purchases.Count(e => e.Id == id) > 0;
        }
    }
}