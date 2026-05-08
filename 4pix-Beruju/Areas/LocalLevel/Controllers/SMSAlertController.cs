using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    [Authorize]
    public class SMSAlertController : Controller
    {
        // GET: LocalLevel/SMSAlert
        int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        public ActionResult Index()
        {
            ListUserForSMSMV smsModel = new ListUserForSMSMV();
            CommonService CS = new CommonService();
            smsModel.ListUserForSMSMVList = new List<ListUserForSMSMV>();
            smsModel.ListUserForSMSMVList = CS.SP_GetPersonDetailsForSMS(1, CurrentUserOfficeId);
            return View(smsModel);
        }



        [HttpPost]
        public ActionResult PostSMS(ListUserForSMSMV model)
        {
            SMSStatus smsStatus = new SMSStatus();
            CommonService CS = new CommonService();
            foreach (var item in model.ListUserForSMSMVList)
            {
                if (item.Ischecked == true)
                {
                    //check limit 
                    int smsLimit = 50;
                    int TotalInserted = CS.TotalNumberInsertedIntoSMS(item.OfficeDetailId);
                    if (TotalInserted > smsLimit)
                    {
                        TempData["Success"] = @"तपाँईलाई दिईएको SMS को संख्याको सिमा धेरै भयो । ";
                        return RedirectToAction("Index");
                    }

                    //check if alreay send sms
                   
                    //int ifAlreadySentSMS = CS.CheckIfAlreadySentSMS(item.OfficeDetailId, item.MobielNumber);
                    int ifAlreadySentSMS = CS.CheckIfAlreadySentSMS(item.OfficeDetailId, item.MobielNumber);
                    if (ifAlreadySentSMS <= 0)
                    {
                        var getResponseTest = GetSendSMS(item.MobielNumber, item.FiscalYearTitleEng, item.TypeName, item.TotalAmount, item.PersonName, item.OFficeName);
                        //var getResponseTest = string.Empty;
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

                            smsStatus.MobileNumber = item.MobielNumber;
                            smsStatus.OfficeId = item.OfficeDetailId;
                            smsStatus.ErrSuccessMessage = "FAILED";
                            smsStatus.ExternalBerujuId = item.InternalOrExternalId;
                            smsStatus.InsertedDate = DateTime.Now;
                            smsStatus.MaxLimitSMS = 50;
                            smsStatus.TowhomDetailsId = item.ToWhomDetailsId;

                        }
                    }
                    else
                    {

                    }


                }

            }
            TempData["Success"] = @"सूची बाट छनोट गरिएका कार्यलय वा व्यक्तिलाई SMS पठाईएको छ ।";
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
            string UniText = @"आ. व. " + FYID + " को " + personName + " को नाममा रु. " + totalAmount + " (" + Berujutype + ") बेरुजु देखिएकोले फर्छ्यौट गर्नुहोला । " + officeName + "";
            string token = @"v2_q4fcIfPX0VL3VK30VDQCGUHmpol.mctc";
            string from = @"TheAlert";
            using (var client = new WebClient())
            {
                string parameters = "?";
                parameters += "from=" + from;
                parameters += "&to=" + to;
                parameters += "&text=" + UniText;
                parameters += "&token=" + token;
                try
                {
                    var responseString = client.DownloadString("https://api.sparrowsms.com/v2/sms/" + parameters);
                    return responseString;
                    //return @"Success";
                }

                catch (Exception)
                {

                    return "SMSERROR";
                }



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