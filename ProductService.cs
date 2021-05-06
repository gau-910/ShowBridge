using ShowBridge.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ShowBridge
{
    public class ProductService : IProductService
    {

        private ModelStateDictionary _modelState;
        private IProductRepository _repository;

        public ProductService(ModelStateDictionary modelState, IProductRepository repository)
        {
            _modelState = modelState;
            _repository = repository;
        }

        protected bool ValidateProduct(ProductModel model, bool blnUpdate = false)
        {
            if (blnUpdate)
            {


                if (_repository.ListProducts().Count(e => e.Name == model.Name && e.Id != model.Id) > 0) //Validation To check Name of product is not assign to other product
                {

                    _modelState.AddModelError("Name", "Product Name is already exists for other Product");


                }
            }
            else
            {
                if (_repository.ListProducts().Count(e => e.Name == model.Name) > 0)
                {
                    _modelState.AddModelError("Name", "Product Name is already exists for other Product");

                }
            }
            return _modelState.IsValid;
        }
        private bool ProductExists(long id)
        {
            return _repository.ListProducts().Count(e => e.Id == id) > 0;
        }
       

        public  async Task<bool> CreateProductAsync(ProductModel model)
        {
            // Validation logic
            if (!ValidateProduct(model))
                return false;

            Product p = ConvertModelTOEntity(model,null);
            try
            {
              return  await _repository.CreateProductAsync(p);
            }
            catch (Exception ex)
            {
                throw;

            }
        }
        public async Task<bool> UpdateProductAsync(ProductModel model)
        {
            try
            {
                // Validation logic
                if (!ValidateProduct(model,true))
                return false;

            Product p =await _repository.FindAsync(model.Id); 
                ConvertModelTOEntity(model,p);
            
              return  await _repository.UpdateProductAsync(p);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(model.Id))
                {
                    return false;
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

        public async Task<bool> DeleteProductAsync(long id)
        {
            try
            {
                Product p = await _repository.FindAsync(id);
               return await _repository.DeleteProductAsync(p);
            }
            catch (Exception ex)
            {
                throw;

            }

        }
        public async Task<Product> GetProductByIdAsunc(long id)
        {
            try
            {
               return await _repository.FindAsync(id);
               
            }
            catch (Exception ex)
            {
                throw;

            }

        }
        private bool ProductExists(string Name)
        {
            return _repository.ListProducts().Count(e => e.Name == Name) > 0;
        }

        public IEnumerable<Product> ListProducts()
        {
            return _repository.ListProducts();
        }

        private Product ConvertModelTOEntity(ProductModel model,Product product)
        {
            if (product==null)
            {
                product = new Product();
            }


            product.Id = model.Id;
            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Active = (model.Active ? "1" : "0");

            return product;
        }

       
    }

    public interface IProductService
    {

        Task<bool> CreateProductAsync(ProductModel model);
        Task<bool> DeleteProductAsync(long id);
        Task<Product> GetProductByIdAsunc(long id);
        IEnumerable<Product> ListProducts();
        Task<bool> UpdateProductAsync(ProductModel model);
    }

}