using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models.Setups;
using System.Web.Services;
using Newtonsoft.Json;

namespace _4pix_Beruju.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Table()
        {
            return View();
        }
        public ActionResult DisplayError()
        {
            ReturnMessageViewModel rms = new ReturnMessageViewModel();
            CommonService cs = new CommonService();
            rms = cs.ShowErrorDetails();
            return View(rms);
        }

        public ActionResult HireEmployee()
        {
            OfficeEmployeeDetail model = new OfficeEmployeeDetail();
            return View(model);
        }
        [HttpPost]
        public ActionResult HireEmployee(OfficeEmployeeDetail model)
        {
            if (ModelState.IsValid)
            {
                RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult ViewAddMore()
        {
            ExpenseTitleAddMoreModel model = new ExpenseTitleAddMoreModel();
            return View(model);
        }

        [WebMethod]
        public ActionResult AddMoreExample(string empdata)
        {
            var serializeData = JsonConvert.DeserializeObject<List<ExpenseTitleAddMoreModel>>(empdata);
            foreach (var data in serializeData)
            {
                //using (var cmd = new SqlCommand("INSERT INTO Employee01 VALUES(@Fname, @Lname,@Email,@CreatedDate)"))
                //{
                //    cmd.CommandType = CommandType.Text;
                //    cmd.Parameters.AddWithValue("@Fname", data.FName);
                //    cmd.Parameters.AddWithValue("@Lname", data.LName);
                //    cmd.Parameters.AddWithValue("@Email", data.EmailId);
                //    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                //    cmd.Connection = con;
                //    if (con.State == ConnectionState.Closed)
                //    {
                //        con.Open();
                //    }
                //    cmd.ExecuteNonQuery();
                //    con.Close();
                //}
            }
            return null;
        }


        public ActionResult ViewAddMoreNext()
        {
            ExpenseTitleAddMoreModel model = new ExpenseTitleAddMoreModel();
            model.ExpenseTitleAddMoreModelList = new List<ExpenseTitleAddMoreModel>();
            return View(model);
        }


        public JsonResult InsertExpenseTitle(List<ExpenseTitleAddMoreModel> expenselist)
        {

            if (expenselist == null)
            {
                expenselist = new List<ExpenseTitleAddMoreModel>();
            }

            //Loop and insert records.
            foreach (ExpenseTitleAddMoreModel customer in expenselist)
            {

            }

            return Json(18);
        }


        public ActionResult DownloadMannual()
        {
            return File("~/Content/UserMannual.pdf", "application/pdf");
        }

    }

}