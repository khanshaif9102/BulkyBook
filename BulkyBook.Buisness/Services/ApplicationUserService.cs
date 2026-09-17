using BulkyBook.Buisness.Services.IServices;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Buisness.Services
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetUserById(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);
        }
    }
}
