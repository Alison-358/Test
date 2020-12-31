using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Services
{
    public interface IUserService : IServiceBase<User>
    {
        User GetByCredential(string email, string password);
    }
}
