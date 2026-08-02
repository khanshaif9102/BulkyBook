using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Buisness.Services.IServices
{
    public interface IProductService
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync(bool includeCategory=false);
        Task<Product> CreateProduct(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        
    }
}
