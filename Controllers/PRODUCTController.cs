using ShowBridge.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ShowBridge.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private IProductService _service;

        public ProductController()
        {
            _service = new ProductService(this.ModelState, new ProductRepository());
        }
      

        // GET: api/product
        [Route("")]
        public IEnumerable<Product> GetProducts()
        {
            return _service.ListProducts();
        }

        // GET: api/product/5
        [Route("{id:long}",Name ="GetProductById")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(long id)
        {
            Product product = await _service.GetProductByIdAsunc(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/product/5
        [Route("update")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct( ProductModel model)
        {
            try
            {

               
                if (await _service.UpdateProductAsync(model))
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }

            catch (Exception ex)
            {

               throw ;
              
            }


        }

        // POST: api/product
        [Route("add")]
        [ResponseType(typeof(ProductModel))]
        public async Task<IHttpActionResult> PostProduct(ProductModel model)
        {          

            try
            {
               
                if (await _service.CreateProductAsync(model))
                {

                    return StatusCode(HttpStatusCode.NoContent);
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

        // DELETE: api/product/5
        [Route("del/{id:long}")]
        [HttpDelete]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(long id)
        {
           
            if (await _service.DeleteProductAsync(id))
            {
                return Ok();
            }
            else {
                return NotFound();

            }
            
        }

        
        
      
    }
}
