using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class RoleService : ServiceBase<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
            : base(roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role GetByUserId(int userId)
        {
           return _roleRepository.GetByUserId(userId);
        }
    }
}
