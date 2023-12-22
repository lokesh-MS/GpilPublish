using GPILWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.REPOSITORY
{
    public interface IUserRepository
    {
        GPIL_USER_MASTER GetUser(string userId);
    }
}