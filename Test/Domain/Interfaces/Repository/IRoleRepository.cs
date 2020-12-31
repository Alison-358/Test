using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Repository
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        //List<Role> GetByFilter(string filter);
        Role GetByUserId(int id);
    }
}
