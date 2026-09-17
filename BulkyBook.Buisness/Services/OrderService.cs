using BulkyBook.Buisness.Services.IServices;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Buisness.Services
{
    public class OrderService : IOrderService
    {
        public Task<OrderHeader> CreateOrderAsync(OrderHeader orderHeader)
        {
            throw new NotImplementedException();
        }

        public Task<OrderHeader?> GetAllOrderAsync(string? userId = null, string? status = null, bool includeUser = false, bool includeOrderDetails = false)
        {
            throw new NotImplementedException();
        }

        public Task<OrderHeader?> GetOrderByIdAsync(int id, bool includeUser = false, bool includeOrderDetails = false)
        {
            throw new NotImplementedException();
        }
    }
}
