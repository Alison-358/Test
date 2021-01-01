using Domain.Dto;
using Service.Utils.Helpers.LoginConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IAuthenticationBusinessService
    {
        Credentials Login(UserLoginDto userLogin);
    }
}
