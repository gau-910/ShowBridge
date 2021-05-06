using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShowBridge
{
    public class ProductRepository : IProductRepository
    {
        private ShowBridgeEntities _entities = new ShowBridgeEntities();

        public IEnumerable<Product> ListProducts()
        {
            return _entities.Products;
        }

        public async Task<Product> FindAsync(long id)
        {
            Product product = await _entities.Products.FindAsync(id);
            return product;
        }
        public async Task<bool> CreateProductAsync(Product product)
        {
            try
            {
                _entities.Products.Add(product);
                await _entities.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                _entities.Entry(product).State = EntityState.Modified;
               await _entities.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw;
               
            }
        }
        public async Task<bool> DeleteProductAsync(Product product)
        {
            try
            {
                _entities.Products.Remove(product);
                await _entities.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;

            }
        }
      
    }

    public interface IProductRepository
    {
        Task<bool> CreateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Product product);
        
        Task<Product> FindAsync(long id);
        IEnumerable<Product> ListProducts();
        Task<bool> UpdateProductAsync(Product product);
    }
}