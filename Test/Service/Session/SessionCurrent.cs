using Service.Interfaces.Generics;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Service.Session
{
    public class SessionCurrent : ISessionCurrent
    {
        private readonly ClaimsPrincipal _principal;

        public SessionCurrent(IPrincipal principal)
        {
            _principal = principal as ClaimsPrincipal;
        }

        public string UserName()
        {
            return _principal.Identity.Name;
        }

        public string UserEmail()
        {
            var email = _principal.FindFirst(ClaimTypes.Email);

            if (email != null)
                return email.Value;
            else
                return null;
        }

        public string UserRole()
        {
            var role = _principal.FindFirst(ClaimTypes.Role);

            if (role != null)
                return role.Value;
            else
                return null;
        }

        public bool IsAdm()
        {
            var role = _principal.FindFirst(ClaimTypes.Role);

            if (role != null)
            {
                if (role.Value.ToUpper() == "admin".ToUpper())
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
