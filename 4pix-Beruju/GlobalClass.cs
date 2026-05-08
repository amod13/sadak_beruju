using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4pix_Beruju
{
    public static class GlobalClass
    {
        public static string RenderRazorViewToString(System.Web.Mvc.Controller controller, string viewname, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewresult;
                viewresult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewname);
                var viewcontext = new ViewContext(controller.ControllerContext, viewresult.View, controller.ViewData, controller.TempData, sw);
                viewresult.View.Render(viewcontext, sw);
                viewresult.ViewEngine.ReleaseView(controller.ControllerContext, viewresult.View);
                return sw.GetStringBuilder().ToString();

            }
        }
        //   return Json(new { success = true,html=GlobalClass.RenderRazorViewToString(this,"viewAll",GetOfficeEmployeeRpt()),message="Saved succcessfully"},JsonRequestBehavior.AllowGet);
    }

}