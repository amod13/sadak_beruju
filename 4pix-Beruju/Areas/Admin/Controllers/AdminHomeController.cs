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
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult Dashboard1()
        {
            return View();
        }
    }
}