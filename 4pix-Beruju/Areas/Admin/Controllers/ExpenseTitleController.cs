using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models;

namespace _4pix_Beruju.Areas.Admin.Controllers
{
    public class ExpenseTitleController : Controller
    {
        // GET: Admin/ExpenseTitle
        public ActionResult Index()
        {
            return View();
        }
    }
}