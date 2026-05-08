using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _4pix_Beruju.Models;

namespace _4pix_Beruju.Services
{
    public class BerujuCommonService
    {
        public List<BerujuNotDoneModel> ListBerujuNotDoneForAdmin(int OfficeId, int InternalOrExternal, int FiscalYearId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<BerujuNotDoneModel> ReturnList = new List<BerujuNotDoneModel>();

                ReturnList = db.Database.SqlQuery<BerujuNotDoneModel>("ListBerujuNotDoneForAdmin {0},{1},{2}", OfficeId, InternalOrExternal, FiscalYearId).ToList();
                return ReturnList;
            }
        }


        public List<BerujuNotDoneModel> ListBerujuNotDone(int OfficeId, int PrimaryId, int InternalOrExternal)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<BerujuNotDoneModel> ReturnList = new List<BerujuNotDoneModel>();

                ReturnList = db.Database.SqlQuery<BerujuNotDoneModel>("ListBerujuNotDone {0},{1},{2}", OfficeId, PrimaryId, InternalOrExternal).ToList();
                return ReturnList;
            }
        }
        public List<BerujuNotDoneModel> ListBerujuNotDoneTopFive(int OfficeId, int InternalOrExternal)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<BerujuNotDoneModel> ReturnList = new List<BerujuNotDoneModel>();

                ReturnList = db.Database.SqlQuery<BerujuNotDoneModel>("ListBerujuNotDoneTopFive {0},{1}", OfficeId, InternalOrExternal).ToList();
                return ReturnList;
            }
        }


        public ReturnMessageViewModel InsertBerujunotdone(BerujuNotDoneModel model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();


                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var CreatedByParam = new SqlParameter { ParameterName = "@CreatedBy", Value = model.CreatedBy };
                var UploadFileUrlParam = new SqlParameter { ParameterName = "@UploadFileUrl", Value = model.UploadFileUrl };

                var NotDoneRemarksParam = new SqlParameter { ParameterName = "@NotDoneRemarks", Value = model.NotDoneRemarks == null ? string.Empty : model.NotDoneRemarks };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
                var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = model.InternalOrExternal };
                var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = model.KoshTypeId };
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

                var result = db.Database.ExecuteSqlCommand("exec InsertBerujunotdone @OfficeId,@CreatedBy,@UploadFileUrl,@NotDoneRemarks,@FiscalYearId,@InternalOrExternal,@KoshTypeId,@Message OUT,@PrimaryId OUT",
                    OfficeIdParam, CreatedByParam, UploadFileUrlParam, NotDoneRemarksParam, FiscalYearIdParam, InternalOrExternalParam, KoshTypeIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel UpdateBerujunotdone(BerujuNotDoneModel model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

                var BerujuNotDoneIdParam = new SqlParameter { ParameterName = "@BerujuNotDoneId", Value = model.BerujuNotDoneId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var CreatedByParam = new SqlParameter { ParameterName = "@CreatedBy", Value = model.CreatedBy };
                var UploadFileUrlParam = new SqlParameter { ParameterName = "@UploadFileUrl", Value = model.UploadFileUrl };

                var NotDoneRemarksParam = new SqlParameter { ParameterName = "@NotDoneRemarks", Value = model.NotDoneRemarks == null ? string.Empty : model.NotDoneRemarks };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
                var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = model.KoshTypeId };
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

                var result = db.Database.ExecuteSqlCommand("exec UpdateBerujunotdone @BerujuNotDoneId,@UploadFileUrl,@NotDoneRemarks,@FiscalYearId,@KoshTypeId,@Message OUT,@PrimaryId OUT",
                    BerujuNotDoneIdParam, OfficeIdParam, UploadFileUrlParam, NotDoneRemarksParam, FiscalYearIdParam,KoshTypeIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }


        public ReturnMessageViewModel DeleteBerujunotdone(int BerujuNotDoneId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var BerujuNotDoneIdParam = new SqlParameter { ParameterName = "@BerujuNotDoneId", Value = BerujuNotDoneId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec DeleteBerujunotdone @BerujuNotDoneId,@Message OUT", BerujuNotDoneIdParam, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = 0;
                return returnModel;
            }

        }

    }
}