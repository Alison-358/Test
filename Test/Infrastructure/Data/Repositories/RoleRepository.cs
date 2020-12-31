using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public Role GetByUserId(int userId)
        {
            using (_context = new DatabaseContext(_databaseContext))
            {
                return _context.User.Where(p => p.Id == userId).Select(p => p.Role).FirstOrDefault();
            }
        }
    }
}
