using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Repositories
{
    public interface IAddressRepository : IRepositoryBase<Address>
    {
        List<Address> GetAddressListByFilter(string filter);
    }
}
