using BAISTGolfClub.API.Interfaces;
using BAISTGolfClub.Data.DBContext;
using BAISTGolfClub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAISTGolfClub.API.Services
{
    public class UserService : IUserService
    {
        private readonly BAISTGolfClubContext _context;

        public UserService(BAISTGolfClubContext context)
        {
            this._context = context;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await this._context.User.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await this._context.User.FindAsync(id);
        }
    }
}
