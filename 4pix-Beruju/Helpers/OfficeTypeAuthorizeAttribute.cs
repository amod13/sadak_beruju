using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4pix_Beruju.Helpers
{
    public class OfficeTypeAuthorizeAttribute : AuthorizeAttribute
    {
        public int RequiredOfficeType { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            int officeType =
                _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserOfficeTypeId();

            return officeType == RequiredOfficeType;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Logged in but not authorized
            filterContext.Result = new HttpStatusCodeResult(403);
        }
    }
}
