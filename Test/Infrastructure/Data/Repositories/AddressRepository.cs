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
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public List<Address> GetAddressListByFilter(string filter)
        {
            using (_context = new DatabaseContext(_databaseContext))
            {
                var item = _context;

                return item.Address
                           .Where(p => string.IsNullOrEmpty(filter) || p.Description.Contains(filter) || p.Complement.Contains(filter)
                                    || p.Neighborhood.Contains(filter) || p.Number.Contains(filter)
                                 )
                           .ToList();
            }

        }
    }
}
