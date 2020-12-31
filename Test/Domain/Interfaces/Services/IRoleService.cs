using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Services
{
    interface IRoleService : IServiceBase<Role>
    {
        Role GetByUserId(int userId);
    }
}
