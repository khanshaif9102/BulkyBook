using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Buisness.Services.IServices
{
    public interface IOrderService
    {
        Task<OrderHeader> CreateOrderAsync(OrderHeader orderHeader);
        Task<OrderHeader?> GetOrderByIdAsync(int id,bool includeUser = false, bool includeOrderDetails = false);

        Task<OrderHeader?> GetAllOrderAsync(string? userId = null,string? status = null,bool includeUser = false, bool includeOrderDetails = false);
    }
}
