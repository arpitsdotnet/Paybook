using Paybook.BusinessLayer.Business;
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
        private readonly IBusinessProcessor _business;

        public UserBusinessHelper(IUserProcessor user, IBusinessProcessor business)
        {
            _user = user;
            _business = business;
        }


    }
}