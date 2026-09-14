using BulkyBook.Buisness.Services.IServices;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Buisness.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ClearCartAsync(string userId)
        {
            var cartItem = await _context.ShoppingCarts.Where(c=>c.ApplicationUserId == userId).ToListAsync();
            if(cartItem.Any())
            {
                _context.ShoppingCarts.RemoveRange(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ShoppingCart?> GetCartByIdAsync(int cartId)
        {
            return await _context.ShoppingCarts.Include(d=>d.Product).FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public async Task<int> GetCartCountAsync(string userId)
        {
            return await _context.ShoppingCarts.Where(c=>c.ApplicationUserId == userId).SumAsync(u=>u.Count);
        }

        public async Task<IEnumerable<ShoppingCart>> GetUserCartItemsAsync(string userId)
        {
            return await _context.ShoppingCarts.Include(d=>d.Product).Where(c => c.ApplicationUserId == userId).ToListAsync();
        }
        public async Task<ShoppingCart> AddToCartAsync(ShoppingCart cart)
        {
            var existingCartItem = await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.ApplicationUserId == cart.ApplicationUserId && c.ProductId == cart.ProductId);
            if (existingCartItem != null)
            {
                existingCartItem.Count += cart.Count;
                await _context.SaveChangesAsync();
                return existingCartItem;
            }
            else
            {
                await _context.ShoppingCarts.AddAsync(cart);
                await _context.SaveChangesAsync();
                return cart;
            }
        }
        public async Task UpdateCartAsync(ShoppingCart cart)
        {
            if (cart.Count <= 0)
            {
                _context.ShoppingCarts.Remove(cart);
            }
            else
            {
                _context.ShoppingCarts.Update(cart);
            }
            await _context.SaveChangesAsync();
        }
    }
}
