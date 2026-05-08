using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;

namespace _4pix_Beruju.Areas.Admin.Controllers
{
    [Authorize]
    public class InternalBerujuController : Controller
    {
        InternalBerujuService IBS = new InternalBerujuService();
        // GET: Admin/InternalBeruju
        public ActionResult Index()
        {
            InternalBeruju model = new InternalBeruju();
            model.InternalBerujuList = new List<InternalBeruju>();
            model.InternalBerujuList = IBS.ListInternalBeruju(1);
            return View(model);
        }


    }
}