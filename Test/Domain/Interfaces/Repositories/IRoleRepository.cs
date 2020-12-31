using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Repositories
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Role GetByUserId(int userId);
    }
}
