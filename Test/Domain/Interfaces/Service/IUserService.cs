using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Service
{
    interface IUserService : IServiceBase<User>
    {
        User GetByCredential(string email, string password);
    }
}
