using Paybook.BusinessLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paybook.Web.MvcUI.Helpers
{
    public class UserBusinessHelper
    {
        private readonly IUserProcessor _user;

        public UserBusinessHelper(IUserProcessor user)
        {
            _user = user;
        }


    }
}