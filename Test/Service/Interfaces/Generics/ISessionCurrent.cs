using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces.Generics
{
    public interface ISessionCurrent
    {
        string UserName();
        string UserEmail();
        string UserRole();
        bool IsAdm();
    }
}
