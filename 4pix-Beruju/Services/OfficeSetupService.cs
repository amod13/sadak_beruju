using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4pix_Beruju.Models;
using _4pix_Beruju.Areas.Admin.Models;
using System.Data.SqlClient;
using System.Data;

namespace _4pix_Beruju.Services
{
    public class OfficeSetupService
    {
        public List<OfficeSetup> ListOfficeByTypeAndProvinceid(int OfficeTypeId, int ProvinceId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<OfficeSetup> ReturnList = new List<OfficeSetup>();
                ReturnList = db.Database.SqlQuery<OfficeSetup>("ListOfficeByTypeAndProvinceid {0},{1}", OfficeTypeId, ProvinceId).ToList();
                return ReturnList;
            }
        }

        public List<OfficeSetup> ListOfficeByTypeAndProvinceid(int OfficeTypeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<OfficeSetup> ReturnList = new List<OfficeSetup>();
                ReturnList = db.Database.SqlQuery<OfficeSetup>("ListOfficeByType {0},{1}", OfficeTypeId).ToList();
                return ReturnList;
            }
        }


        public ReturnMessageViewModel InsertOfficeDetails(OfficeSetup model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var OFficeNameParam = new SqlParameter { ParameterName = "@OFficeName", Value = model.OFficeName };
                var AddressParam = new SqlParameter { ParameterName = "@Address", Value = model.Address };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = model.ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = model.DistrictId };
                var VDCMUNIDParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = model.VDCMUNID };
                var DisplayStatusParam = new SqlParameter { ParameterName = "@DisplayStatus", Value = model.DisplayStatus };
                var UserTypeIdParam = new SqlParameter { ParameterName = "@UserTypeId", Value = model.UserTypeId };
                var MainOfficeIdParam = new SqlParameter { ParameterName = "@MainOfficeId", Value = model.MainOfficeId };
                var OfficeStatusParam = new SqlParameter { ParameterName = "@OfficeStatus", Value = model.OfficeStatus };
                var ProVdcmunTypeIdParam = new SqlParameter { ParameterName = "@ProVdcmunTypeId", Value = model.ProVdcmunTypeId };
                var OfficeEmailParam = new SqlParameter { ParameterName = "@OfficeEmail", Value = model.OfficeEmail };
                var OfficeTypeIdParam = new SqlParameter { ParameterName = "@OfficeTypeId", Value = model.OfficeTypeId };
                var OfficeCodeParam = new SqlParameter { ParameterName = "@OfficeCode", Value = model.OfficeCode };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var PrimaryIdParam = new SqlParameter
                {
                    ParameterName = "@PrimaryId",
                    DbType = DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec InsertOfficeDetail @OFficeName,@Address,@ProvinceId,@DistrictId,@VDCMUNID,@DisplayStatus,@UserTypeId,@MainOfficeId,@OfficeStatus,@ProVdcmunTypeId,@OfficeEmail,@OfficeTypeId,@OfficeCode,@Message OUT,@PrimaryId OUT",
                    OFficeNameParam, AddressParam, ProvinceIdParam, DistrictIdParam, VDCMUNIDParam, DisplayStatusParam, UserTypeIdParam, MainOfficeIdParam, OfficeStatusParam, ProVdcmunTypeIdParam, OfficeEmailParam, OfficeTypeIdParam, OfficeCodeParam, MessageParam, PrimaryIdParam);

                int PKID = PrimaryIdParam.Value == DBNull.Value ? 0 : Convert.ToInt32(PrimaryIdParam.Value);

                //int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }



        public ReturnMessageViewModel SP_InsertDefaultBudgetHeadAndExpenseTitle(int MainOfficeId, int CurrentOfficeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var MainOfficeIdParam = new SqlParameter { ParameterName = "@MainOfficeId", Value = MainOfficeId };
                var CurrentOfficeIdParam = new SqlParameter { ParameterName = "@CurrentOfficeId", Value = CurrentOfficeId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                try
                {
                    var result = db.Database.ExecuteSqlCommand("exec SP_InsertDefaultBudgetHeadAndExpenseTitle @MainOfficeId,@CurrentOfficeId",
                    MainOfficeIdParam, CurrentOfficeIdParam);
                    //int PKID = (int)PrimaryIdParam.Value;
                    //returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                    returnModel.ReturnMessage = "updated successfully";
                }
                catch (Exception)
                {
                    returnModel.ReturnMessage = "Error....";


                }
                return returnModel;


            }

        }


        public ReturnMessageViewModel UpdateOfficeDetails(OfficeSetup model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var OfficeDetailsIdParam = new SqlParameter { ParameterName = "@OfficeDetailsId", Value = model.OfficeDetailId };
                var OFficeNameParam = new SqlParameter { ParameterName = "@OFficeName", Value = model.OFficeName };
                var AddressParam = new SqlParameter { ParameterName = "@Address", Value = model.Address };
                var ProvinceIdParam = new SqlParameter { ParameterName = "@ProvinceId", Value = model.ProvinceId };
                var DistrictIdParam = new SqlParameter { ParameterName = "@DistrictId", Value = model.DistrictId };
                var VDCMUNIDParam = new SqlParameter { ParameterName = "@VDCMUNID", Value = model.VDCMUNID };
                var DisplayStatusParam = new SqlParameter { ParameterName = "@DisplayStatus", Value = model.DisplayStatus };
                var UserTypeIdParam = new SqlParameter { ParameterName = "@UserTypeId", Value = model.UserTypeId };
                var MainOfficeIdParam = new SqlParameter { ParameterName = "@MainOfficeId", Value = model.MainOfficeId };
                var OfficeCodeParam = new SqlParameter { ParameterName = "@OfficeCode", Value = model.OfficeCode };
                var ContactPersonNameParam = new SqlParameter { ParameterName = "@ContactPersonName", Value = model.ContactPerson==null?string.Empty:model.ContactPerson };
                var ContactPersonMobileParam = new SqlParameter { ParameterName = "@ContactPersonMobile", Value = model.ContactPersonMobile==null?string.Empty:model.ContactPersonMobile };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var PrimaryIdParam = new SqlParameter
                {
                    ParameterName = "@PrimaryId",
                    DbType = DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec UpdateOfficeDetail @OfficeDetailsId,@OFficeName,@Address,@ProvinceId,@DistrictId,@VDCMUNID,@DisplayStatus,@UserTypeId,@MainOfficeId,@OfficeCode,@ContactPersonName,@ContactPersonMobile,@Message OUT,@PrimaryId OUT",
                    OfficeDetailsIdParam, OFficeNameParam, AddressParam, ProvinceIdParam, DistrictIdParam, VDCMUNIDParam, DisplayStatusParam, UserTypeIdParam, MainOfficeIdParam, OfficeCodeParam, ContactPersonNameParam, ContactPersonMobileParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel DeleteOfficeDetail(OfficeSetup model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

                var OfficeCodeParam = new SqlParameter { ParameterName = "@OfficeCode", Value = model.OfficeCode };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };



                var result = db.Database.ExecuteSqlCommand("exec DeleteOfficeDetail @OFficeName,@Message OUT",
                    OfficeCodeParam, MessageParam);
                int PKID = model.OfficeDetailId;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel DeleteOfficesByOfficeId(OfficeSetup model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeDetailId };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };



                var result = db.Database.ExecuteSqlCommand("exec DeleteOfficeDetail @OfficeId,@Message OUT",
                    OfficeIdParam, MessageParam);
                int PKID = model.OfficeDetailId;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }



    }
}