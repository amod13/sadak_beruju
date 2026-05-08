using _4pix_Beruju.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Services;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace _4pix_Beruju.Areas.Admin.Controllers
{


    [Authorize]
    public class SMSActionController : Controller
    {
        // GET: Admin/SMSAction
        public ActionResult Index()
        {
            ListUserForSMSMV smsModel = new ListUserForSMSMV();
            CommonService CS = new CommonService();
            smsModel.ListUserForSMSMVList = new List<ListUserForSMSMV>();
            smsModel.ListUserForSMSMVList = CS.SP_GetPersonDetailsForSMS(1, 1019);
            return View(smsModel);
        }

        [HttpPost]
        public ActionResult PostSMS(ListUserForSMSMV model)
        {
            var getResponseTest = GetSendSMS("9858099000", "2076/77", @"पेश्की", 100m, "राम कुमार शर्मा", @" प्रदेश लेखा नियन्त्रक कार्यालय");
            //var getResponseTest = GetSendSMS("9841291570", "2076/77", @"पेश्की", 100m, "राम कुमार शर्मा", @" प्रदेश लेखा नियन्त्रक कार्यालय");
            CommonService CS = new CommonService();
            foreach (var item in model.ListUserForSMSMVList)
            {
                if (item.Ischecked == true)
                {
                    SMSStatus smsStatus = new SMSStatus();
                    smsStatus.MobileNumber = item.MobielNumber;
                    smsStatus.OfficeId = item.OfficeDetailId;
                    smsStatus.ErrSuccessMessage = getResponseTest;
                    smsStatus.ExternalBerujuId = item.InternalOrExternalId;
                    smsStatus.InsertedDate = DateTime.Now;
                    smsStatus.MaxLimitSMS = 50;
                    smsStatus.TowhomDetailsId = item.ToWhomDetailsId;
                    try
                    {

                        CS.InsertSMSDetails(smsStatus);
                    }
                    catch (Exception)
                    {


                    }

                }

            }
            return RedirectToAction("Index");
        }

        public ActionResult SendSMSFromList()
        {
            ListUserForSMSMV smsModel = new ListUserForSMSMV();
            CommonService CS = new CommonService();
            smsModel.ListUserForSMSMVList = new List<ListUserForSMSMV>();
            smsModel.ListUserForSMSMVList = CS.SP_GetPersonDetailsForSMS(1, 1);
            foreach (var item in smsModel.ListUserForSMSMVList)
            {
                try
                {
                    var getResponseTest = GetSendSMS(item.MobielNumber, item.FiscalYearTitleEng, item.TypeName, item.TotalAmount, item.PersonName, item.OFficeName);
                    //insert into sms table
                    SMSStatus smsStatus = new SMSStatus();
                    smsStatus.MobileNumber = item.MobielNumber;
                    smsStatus.OfficeId = item.OfficeDetailId;
                    smsStatus.ErrSuccessMessage = "";
                    smsStatus.ExternalBerujuId = item.InternalOrExternalId;
                    smsStatus.InsertedDate = DateTime.Now;
                    smsStatus.MaxLimitSMS = 50;
                    smsStatus.TowhomDetailsId = item.ToWhomDetailsId;
                    CS.InsertSMSDetails(smsStatus);
                }
                catch (Exception e)
                {
                    //insert into sms table
                    SMSStatus smsStatus = new SMSStatus();
                    smsStatus.MobileNumber = item.MobielNumber;
                    smsStatus.OfficeId = item.OfficeDetailId;
                    smsStatus.ErrSuccessMessage = "SMSFAIL";
                    smsStatus.ExternalBerujuId = item.InternalOrExternalId;
                    smsStatus.InsertedDate = DateTime.Now;
                    smsStatus.MaxLimitSMS = 50;
                    smsStatus.TowhomDetailsId = item.ToWhomDetailsId;
                    CS.InsertSMSDetails(smsStatus);
                }
            }


            return View(smsModel);
        } 

        private static string GetSendSMS(string to, string FYID, string Berujutype, decimal? totalAmount, string personName, string officeName)
        {
            
            string UniText = @"आ. व. " + FYID + " को " + personName + " को नाममा रु. " + totalAmount + " ("+ Berujutype + ") बेरुजु देखिएकोले फर्छ्यौट गर्नुहोला । " + officeName+"";
            string token = @"v2_q4fcIfPX0VL3VK30VDQCGUHmpol.mctc";
            string from = @"TheAlert";
            using (var client = new WebClient())
            {
                string parameters = "?";
                parameters += "from=" + from;
                parameters += "&to=" + to;
                parameters += "&text=" + UniText;
                parameters += "&token=" + token;
                //var responseString = client.DownloadString("https://api.sparrowsms.com/v2/sms/" + parameters);
                //return responseString;
                return "";
               
            }
        }

        private static string PostSendSMS(string to, string FYID, string Berujutype, decimal? totalAmount, string personName)
        {
            string UniText = @"आ. व. " + FYID + " को " + personName + " नाममा रु. " + totalAmount + "  (असुल उपर/ पेस्कि/ )बेरुजु कायम रहेकाले प्रमाण सहित कार्यालयमा उपस्थित हुन अनुरोध छ ।";
            string token = @"v2_q4fcIfPX0VL3VK30VDQCGUHmpol.mctc";
            string from = @"THE_Alert";
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["from"] = from;
                values["token"] = token;
                values["to"] = to;
                values["text"] = UniText;
                var response = client.UploadValues("http://api.sparrowsms.com/v2/sms/", "Post", values);
                var responseString = Encoding.Default.GetString(response);
                return responseString;
            }
        }




    }
}