using BAISTGolfClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAISTGolfClub.API.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid id);
    }
}
