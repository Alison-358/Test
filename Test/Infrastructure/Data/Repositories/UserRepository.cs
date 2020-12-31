using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public User GetByCredential(string email, string password)
        {
            using (_context = new DatabaseContext(_databaseContext))
            {
                var item = _context;

                var res = item.User
                              .Include(x => x.Role)
                              .Where(p => p.Email == email && p.Password == password)
                              .FirstOrDefault();

                return res;
            }
        }
    }
}
