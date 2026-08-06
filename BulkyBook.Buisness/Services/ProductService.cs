using BulkyBook.Buisness.Services.IServices;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Buisness.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool includeCategory = false)
        {
            if (includeCategory)
            {
                return await _context.Products.Include(p=>p.Category).ToListAsync();
            }
            else
            {
                return await _context.Products.ToListAsync();
            }
        }
        public async Task<Product?> GetProductByIdAsync(int id, bool includeCategory = false)
        {
            if (includeCategory)
            {
                return await _context.Products.Include(p => p.Category).Where(d=>d.Id == id).FirstOrDefaultAsync();
            }
            else
            {
                return await _context.Products.Where(d => d.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product {id} not found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }




        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
