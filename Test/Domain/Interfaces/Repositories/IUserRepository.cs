using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetByCredential(string email, string password);
    }
}
