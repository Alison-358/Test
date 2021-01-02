using Domain.Dto;
using Domain.Interfaces.Services;
using Service.Interfaces;
using Service.Utils.Helpers.LoginConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Services
{
    public class AuthenticationBusinessService : IAuthenticationBusinessService
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly Token _token;
        private readonly Signing _signing;

        public AuthenticationBusinessService(IRoleService roleService, IUserService userService, Token token, Signing signing)
        {
            _roleService = roleService;
            _userService = userService;
            _token = token;
            _signing = signing;
        }

        public Credentials Login(UserLoginDto userLogin)
        {
            userLogin = (UserLoginDto)_userService.GetByCredential(userLogin.Email, userLogin.Password);

            if (userLogin == null)
                throw new InvalidOperationException("User not found, check credentials.");

            LoginConfig loginConfig = new LoginConfig();

            return loginConfig.LoginCredentials(userLogin, this._token, this._signing);
        }
    }
}
