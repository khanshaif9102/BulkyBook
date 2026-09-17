using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Buisness.Services.IServices
{
    public interface IApplicationUserService
    {
        Task<ApplicationUser?> GetUserById(string userId);
    }
}
