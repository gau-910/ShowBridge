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
using ShowBridge;
using ShowBridge.Models;

namespace ShowBridge.Controllers
{
    public class PRODUCTController : ApiController
    {
        private ShowBridgeEntities db = new ShowBridgeEntities();

        // GET: api/PRODUCT
        public IQueryable<PRODUCT> GetPRODUCTs()
        {
            return db.PRODUCTs;
        }

        // GET: api/PRODUCT/5
        [ResponseType(typeof(PRODUCT))]
        public async Task<IHttpActionResult> GetPRODUCT(long id)
        {
            PRODUCT pRODUCT = await db.PRODUCTs.FindAsync(id);
            if (pRODUCT == null)
            {
                return NotFound();
            }

            return Ok(pRODUCT);
        }

        // PUT: api/PRODUCT/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPRODUCT(long id, ProductModel model)
        {
            try
            {
                bool IsValid = true;
                if (!ModelState.IsValid)
                {
                    IsValid = false;
                    goto returnStatus;
                }
                if (id != model.ID)
                {
                    IsValid = false;
                    ModelState.AddModelError("ID", "Id in Url does not match with Id in body of Url");
                    goto returnStatus;
                }
                if (db.PRODUCTs.Count(e => e.NAME == model.NAME && e.ID != model.ID) > 0) //Validation To check Name of product is not assign to other product
                {
                    IsValid = false;
                    ModelState.AddModelError("Name", "Product Name is Already exists for other Product");
                    goto returnStatus;

                }
                PRODUCT p = ConvertModelTOEntity(model);
                db.Entry(p).State = EntityState.Modified;

                await db.SaveChangesAsync();
            returnStatus:
                if (IsValid)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PRODUCTExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

           
        }

        // POST: api/PRODUCT
        [ResponseType(typeof(PRODUCT))]
        public async Task<IHttpActionResult> PostPRODUCT(ProductModel model)
        {
            PRODUCT p = ConvertModelTOEntity(model);


            try
            {
                bool IsValid = true;
                if (!ModelState.IsValid)
                {
                    IsValid = false;
                    goto returnStatus;
                }
                if (db.PRODUCTs.Count(e => e.NAME == model.NAME) > 0)//Validation To check Name of product is not assign to other product
                {
                    IsValid = false;
                    ModelState.AddModelError("Name", "Product Name is Already exists for other Product");
                    goto returnStatus;

                }

                db.PRODUCTs.Add(p);
                await db.SaveChangesAsync();
            returnStatus:
                if (IsValid)
                {
                    return CreatedAtRoute("DefaultApi", new { id = p.ID }, p);
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // DELETE: api/PRODUCT/5
        [ResponseType(typeof(PRODUCT))]
        public async Task<IHttpActionResult> DeletePRODUCT(long id)
        {
            PRODUCT pRODUCT = await db.PRODUCTs.FindAsync(id);
            if (pRODUCT == null)
            {
                return NotFound();
            }

            db.PRODUCTs.Remove(pRODUCT);
            await db.SaveChangesAsync();

            return Ok(pRODUCT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PRODUCTExists(long id)
        {
            return db.PRODUCTs.Count(e => e.ID == id) > 0;
        }
        private bool PRODUCTExists(string Name)
        {
            return db.PRODUCTs.Count(e => e.NAME == Name) > 0;
        }

        private PRODUCT ConvertModelTOEntity(ProductModel model)
        {
            return new PRODUCT
            {
                ID = model.ID,
                NAME = model.NAME,
                PRICE = model.PRICE,
                DESCRIPTION = model.DESCRIPTION,
                ACTIVE = (model.ACTIVE ? "1" : "0")

            };
        }
    }
}
