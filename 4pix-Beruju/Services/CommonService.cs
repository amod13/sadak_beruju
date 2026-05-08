using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using _4pix_Beruju.Models;
using _4pix_Beruju.Models.Setups;
using _4pix_Beruju.Models.ViewModel;

namespace _4pix_Beruju.Services
{
    public class CommonService
    {
        public List<BerujuType> GetBerujuTypeList()
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<BerujuType> ReturnList = new List<BerujuType>();
                ReturnList = db.Database.SqlQuery<BerujuType>("ListBerujuType").ToList();
                return ReturnList;
            }
        }

        public List<BerujuSubType> GetBerujuSubTypeList()
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<BerujuSubType> ReturnList = new List<BerujuSubType>();
                ReturnList = db.Database.SqlQuery<BerujuSubType>("ListBerujuSubType").ToList();
                return ReturnList;
            }
        }



        public List<BerujuSubTypeChild> GetBerujuSubTypeChildList()
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<BerujuSubTypeChild> ReturnList = new List<BerujuSubTypeChild>();
                ReturnList = db.Database.SqlQuery<BerujuSubTypeChild>("ListBerujuSubTypeChild").ToList();
                return ReturnList;
            }
        }


        public ReturnMessageViewModel ShowErrorDetails()
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                int CurrentLoginUserOFficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
                var CurrentLoginUserOFficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = CurrentLoginUserOFficeId };
                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                rms = db.Database.SqlQuery<ReturnMessageViewModel>("ShowErrorDetails @OfficeId", CurrentLoginUserOFficeIdParam).FirstOrDefault();
                if (rms == null)
                {
                    return new ReturnMessageViewModel();
                }
                return rms;
            }
        }

        public ReturnMessageViewModel InsertBerujuType(BerujuType model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var TypeNameParam = new SqlParameter { ParameterName = "@TypeName", Value = model.TypeName };
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = true };
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

                var result = db.Database.ExecuteSqlCommand("exec InsertBerujuType @TypeName,@BerujuStatus,@Message OUT,@PrimaryId OUT", TypeNameParam, BerujuStatusParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel UpdateBerujuType(BerujuType model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var TypeNameParam = new SqlParameter { ParameterName = "@TypeName", Value = model.TypeName };
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = true };
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

                var result = db.Database.ExecuteSqlCommand("exec UpdateBerujuType @BerujuTypeId,@TypeName,@BerujuStatus,@Message OUT,@PrimaryId OUT", BerujuTypeIdParam, TypeNameParam, BerujuStatusParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }
        }

        public ReturnMessageViewModel DeleteBerujuType(int id)
        {
            ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

            return returnModel;

        }

        public ReturnMessageViewModel InsertBerujuSubType(BerujuSubType model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var SubTitleParam = new SqlParameter { ParameterName = "@SubTitle", Value = model.SubTitle };
                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
    
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = true };
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

                var result = db.Database.ExecuteSqlCommand("exec InsertBerujuSubType @SubTitle,@BerujuTypeId,@BerujuStatus,@Message OUT,@PrimaryId OUT", SubTitleParam,BerujuTypeIdParam, BerujuStatusParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel UpdateBerujuSubType(BerujuSubType model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var BerujuSubTitleIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleId", Value = model.BerujuSubTitleId };
                var SubTitleParam = new SqlParameter { ParameterName = "@SubTitle", Value = model.SubTitle };
                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = true };
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

                var result = db.Database.ExecuteSqlCommand("exec UpdateBerujuSubType @BerujuSubTitleId,@SubTitle,@BerujuTypeId,@BerujuStatus,@Message OUT,@PrimaryId OUT", BerujuSubTitleIdParam, SubTitleParam, BerujuTypeIdParam  ,BerujuStatusParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }
        }


        public ReturnMessageViewModel DeleteBerujuTSubype(int id)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var BerujuSubTypeIdPAram = new SqlParameter { ParameterName = "@BerujuSubTypeId", Value = id };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec DeleteBerujuSubType @BerujuSubTypeId,@Message OUT", BerujuSubTypeIdPAram, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = 0;
                return returnModel;
            }

        }
        public ReturnMessageViewModel InsertBerujuSubTypeChild(BerujuSubTypeChild model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var SubTitleParam = new SqlParameter { ParameterName = "@SubTitleChild", Value = model.SubTitleChild };
                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var BerujuSubTittleIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleId", Value = model.BerujuSubTitleId };
               

                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = true };
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

                var result = db.Database.ExecuteSqlCommand("exec InsertBerujuSubTypeChild @SubTitleChild,@BerujuTypeId,@BerujuSubTitleId,@BerujuStatus,@Message OUT,@PrimaryId OUT", SubTitleParam, BerujuTypeIdParam,BerujuSubTittleIdParam, BerujuStatusParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel UpdateBerujuSubTypeChild(BerujuSubTypeChild model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var BerujuSubTitleChildIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleChildId", Value = model.BerujuSubTitleChildId };
                var SubTitleChildParam = new SqlParameter { ParameterName = "@SubTitleChild", Value = model.SubTitleChild };
                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var BerujuSubTitleIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleId", Value = model.BerujuSubTitleId };
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = true };
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

                var result = db.Database.ExecuteSqlCommand("exec UpdateBerujuSubTypeChild @BerujuSubTitleChildId,@SubTitleChild,@BerujuTypeId,@BerujuSubTitleId,@BerujuStatus,@Message OUT,@PrimaryId OUT", BerujuSubTitleChildIdParam, SubTitleChildParam, BerujuTypeIdParam, BerujuSubTitleIdParam, BerujuStatusParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }
        }


        public ReturnMessageViewModel DeleteBerujuTSubypeChild(int id)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var BerujuSubTypeChildIdPAram = new SqlParameter { ParameterName = "@BerujuSubTypeChildId", Value = id };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec DeleteBerujuSubTypeChild @BerujuSubTypeChildId,@Message OUT", BerujuSubTypeChildIdPAram, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = 0;
                return returnModel;
            }

        }

        #region Employee Auditor Setup

        public List<EmployeeAuditor> GetEmployeeOrAuditorDetails(int OfficeId, int EmpType, DateTime checkDate)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<EmployeeAuditor> ReturnList = new List<EmployeeAuditor>();
                ReturnList = db.Database.SqlQuery<EmployeeAuditor>("GetEmployeeOrAuditorDetails {0},{1},{2}", OfficeId, EmpType, checkDate).ToList();
                return ReturnList;
            }
        }

        public List<EmployeeAuditor> ListEmployeeOrAuditorDetails(int OfficeId, int EmpType)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<EmployeeAuditor> ReturnList = new List<EmployeeAuditor>();
                ReturnList = db.Database.SqlQuery<EmployeeAuditor>("ListEmployeeOrAuditorDetails {0},{1}", OfficeId, EmpType).ToList();
                return ReturnList;
            }
        }


        public ReturnMessageViewModel InsertEmployeeAuditorDetails(EmployeeAuditor model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

                var EmpNameParam = new SqlParameter { ParameterName = "@EmpName", Value = model.EmpName };
                var FromDurationParam = new SqlParameter { ParameterName = "@FromDuration", Value = model.FromDuration };
                var ToDurationParam = new SqlParameter { ParameterName = "@ToDuration", Value = model.ToDuration };
                var EmpTypeParam = new SqlParameter { ParameterName = "@EmpType", Value = model.EmpType };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var AuditorPostParam = new SqlParameter { ParameterName = "@AuditorPost", Value = model.AuditorPost == null ? string.Empty : model.AuditorPost };
                //string aditPost = model.AuditorPost;
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
                var result = db.Database.ExecuteSqlCommand("exec InsertEmployeeAuditorDetails @EmpName,@FromDuration,@ToDuration,@EmpType,@OfficeId,@AuditorPost ,@Message OUT,@PrimaryId OUT", EmpNameParam, FromDurationParam, ToDurationParam, EmpTypeParam, OfficeIdParam, AuditorPostParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }
        public ReturnMessageViewModel UpdateEmployeeAuditorDetails(EmployeeAuditor model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var EmployeeAuditorIdParam = new SqlParameter { ParameterName = "@EmployeeAuditorId", Value = model.EmployeeAuditorDetailsId };
                var EmpNameParam = new SqlParameter { ParameterName = "@EmpName", Value = model.EmpName };
                var FromDurationParam = new SqlParameter { ParameterName = "@FromDuration", Value = model.FromDuration };
                var ToDurationParam = new SqlParameter { ParameterName = "@ToDuration", Value = model.ToDuration };
                var EmpTypeParam = new SqlParameter { ParameterName = "@EmpType", Value = model.EmpType };
                var AuditorPostParam = new SqlParameter { ParameterName = "@AuditorPost", Value = model.AuditorPost == null ? string.Empty : model.AuditorPost };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };

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
                var result = db.Database.ExecuteSqlCommand("exec UpdateEmployeeAuditorDetails @EmployeeAuditorId,@EmpName,@FromDuration,@ToDuration,@EmpType,@AuditorPost,@OfficeId,@Message OUT,@PrimaryId OUT", EmployeeAuditorIdParam, EmpNameParam, FromDurationParam, ToDurationParam, EmpTypeParam, AuditorPostParam, OfficeIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }





        public List<ManagerOrAuditorNameViewModel> GetEmployeeAndAuditorNameByVoucher(int OfficeId, int EmployeeType, DateTime checkDate)
        {
            List<ManagerOrAuditorNameViewModel> returnList = new List<ManagerOrAuditorNameViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<ManagerOrAuditorNameViewModel>("GetEmployeeOrAuditorDetails {0},{1},{2}", OfficeId, 0, checkDate).ToList();
                return returnList;
            }

        }


        public DateTime GetStartEndDateFromFiscalYearId(int FYID, string StartOrEnd)
        {
            DateTime ReturnDate = new DateTime();
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    if (StartOrEnd == "Start")
                    {
                        ReturnDate = db.Database.SqlQuery<DateTime>("Select StartFrom From FiscalYearRecord where FiscalYearId= @id", new SqlParameter("@id", FYID))
                                .FirstOrDefault();

                    }
                    else
                    {
                        ReturnDate = db.Database.SqlQuery<DateTime>("Select EndDate From FiscalYearRecord where FiscalYearId=@id", new SqlParameter("@id", FYID))
                                .FirstOrDefault();

                    }
                }

                catch (Exception)
                {

                    ReturnDate = new DateTime();
                    ReturnDate = ReturnDate.AddYears(-100);
                }


            }

            return ReturnDate;
        }


        public int GetFiscalYearRecordIdFromStartEndDate(DateTime Startdate, DateTime Enddate)
        {
            int FYID = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {

                    FYID = db.Database.SqlQuery<int>(@"select FiscalYearId From FiscalYearRecord where 
                            StartFrom between '" + Startdate + "' and '" + Enddate + "' or EndDate between '" + Startdate + "' and '" + Enddate + "'").FirstOrDefault();
                }

                catch (Exception)
                {

                    FYID = 0;
                }


            }

            return FYID;
        }


        public ReturnMessageViewModel DeleteOfficeManager(int EmployeeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var EmployeeAuditorIdParam = new SqlParameter { ParameterName = "@EmployeeOrAuditorId", Value = EmployeeId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec DeleteOfficeManager @EmployeeOrAuditorId,@Message OUT", EmployeeAuditorIdParam, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = 0;
                return returnModel;
            }

        }
        public ReturnMessageViewModel DeleteOfficeAccountant(int EmployeeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var EmployeeAuditorIdParam = new SqlParameter { ParameterName = "@EmployeeOrAuditorId", Value = EmployeeId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec DeleteOfficeAccountant @EmployeeOrAuditorId,@Message OUT", EmployeeAuditorIdParam, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = 0;
                return returnModel;
            }

        }
        public ReturnMessageViewModel DeleteOfficeAuditor(int EmployeeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var EmployeeAuditorIdParam = new SqlParameter { ParameterName = "@EmployeeOrAuditorId", Value = EmployeeId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec DeleteOfficeAuditor @EmployeeOrAuditorId,@Message OUT", EmployeeAuditorIdParam, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = 0;
                return returnModel;
            }

        }


        #endregion


        #region Chart Of Account

        public List<ExpenseTitleViewModel> ListExpenseTitleByOfficeId(int OfficeId)
        {
            List<ExpenseTitleViewModel> returnList = new List<ExpenseTitleViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {
                returnList = db.Database.SqlQuery<ExpenseTitleViewModel>("ListExpenseTitleByOfficeId {0}", OfficeId).ToList();
                return returnList;
            }

        }


        public ReturnMessageViewModel InsertExpenseTitleDetails(ExpenseTitleViewModel model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

                var ExpenseTitleNameParam = new SqlParameter { ParameterName = "@ExpenseTitleName", Value = model.ExpenseTitleName };
                var ExpenseCodeParam = new SqlParameter { ParameterName = "@ExpenseCode", Value = model.ExpenseCode };
                var BudgetSubTitleIdParam = new SqlParameter { ParameterName = "@BudgetSubTitleId", Value = model.BudgetSubTitleId };
                var ExpenseStatusParam = new SqlParameter { ParameterName = "@ExpenseStatus", Value = model.ExpenseStatus };
                var OfficeIdParamParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };

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
                var result = db.Database.ExecuteSqlCommand("exec InsertExpenseTitle @ExpenseTitleName,@ExpenseCode,@BudgetSubTitleId,@ExpenseStatus,@OfficeId ,@Message OUT,@PrimaryId OUT",
                    ExpenseTitleNameParam, ExpenseCodeParam, BudgetSubTitleIdParam, ExpenseStatusParam, OfficeIdParamParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel UpdateExpenseTitleDetails(ExpenseTitleViewModel model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var ExpenseTitleIdParam = new SqlParameter { ParameterName = "@ExpenseTitleId", Value = model.ExpenseTitleId };
                var ExpenseTitleNameParam = new SqlParameter { ParameterName = "@ExpenseTitleName", Value = model.ExpenseTitleName };
                var ExpenseCodeParam = new SqlParameter { ParameterName = "@ExpenseCode", Value = model.ExpenseCode };
                var BudgetSubTitleIdParam = new SqlParameter { ParameterName = "@BudgetSubTitleId", Value = model.BudgetSubTitleId };

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
                var result = db.Database.ExecuteSqlCommand("exec UpdateExpenseTitle @ExpenseTitleId,@ExpenseTitleName,@ExpenseCode,@BudgetSubTitleId,@Message OUT,@PrimaryId OUT",
                    ExpenseTitleIdParam, ExpenseTitleNameParam, ExpenseCodeParam, BudgetSubTitleIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        #endregion



        public bool InsertErrorDetails(string ErrorDetailsstr)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                int CurrentLoginUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var ErrorDetailsparam = new SqlParameter { ParameterName = "@ErrorDetails", Value = ErrorDetailsstr == null ? string.Empty : ErrorDetailsstr };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = CurrentLoginUserOfficeId };
                try
                {
                    var result = db.Database.ExecuteSqlCommand("exec InsertErrorDetails @ErrorDetails,@OfficeId", ErrorDetailsparam, OfficeIdParam);

                }
                catch (Exception)
                {

                    return false;
                }


            }
            return false;

        }

        public List<SamparikshadLetterOfficeSetupViewModel> GetSamparikshadletterofficesetupList(int OfficeId, int SetupType)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<SamparikshadLetterOfficeSetupViewModel> ReturnList = new List<SamparikshadLetterOfficeSetupViewModel>();
                ReturnList = db.Database.SqlQuery<SamparikshadLetterOfficeSetupViewModel>("GetSamparikshadletterofficesetupList {0},{1}", OfficeId, SetupType).ToList();
                return ReturnList;
            }
        }

        public ReturnMessageViewModel InsertSamparikshadlettersetup(LetterSetupViewModel model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var OfficeNameParam = new SqlParameter { ParameterName = "@OfficeName", Value = model.ObjSamparikshadLetterOfficeSetupViewModel.OfficeName };
                var OfficeAddressParam = new SqlParameter { ParameterName = "@OfficeAddress", Value = model.ObjSamparikshadLetterOfficeSetupViewModel.OfficeAddress };
                var SetupStatusParam = new SqlParameter { ParameterName = "@SetupStatus", Value = true };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.ObjSamparikshadLetterOfficeSetupViewModel.OfficeId };
                var SetupTypeParam = new SqlParameter { ParameterName = "@SetupType", Value = model.ObjSamparikshadLetterOfficeSetupViewModel.BerujuTypeId };


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

                var result = db.Database.ExecuteSqlCommand("exec InsertSamparikshadlettersetup @OfficeName,@OfficeAddress,@SetupStatus,@OfficeId,@SetupType,@Message OUT,@PrimaryId OUT",
                    OfficeNameParam, OfficeAddressParam, SetupStatusParam, OfficeIdParam, SetupTypeParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }


        public ReturnMessageViewModel UpdateSamparikshadlettersetup(LetterSetupViewModel model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var SamparikshadLetterSetupIdParam = new SqlParameter { ParameterName = "@SamparikshadLetterSetupId", Value = model.ObjSamparikshadLetterOfficeSetupViewModel.SamparikshadLetterSetupId };
                var OfficeNameParam = new SqlParameter { ParameterName = "@OfficeName", Value = model.ObjSamparikshadLetterOfficeSetupViewModel.OfficeName };
                var OfficeAddressParam = new SqlParameter { ParameterName = "@OfficeAddress", Value = model.ObjSamparikshadLetterOfficeSetupViewModel.OfficeAddress };
                var SetupStatusParam = new SqlParameter { ParameterName = "@SetupStatus", Value = true };
                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@SetupTypeId", Value = model.ObjSamparikshadLetterOfficeSetupViewModel.BerujuTypeId };
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

                var result = db.Database.ExecuteSqlCommand("exec UpdateSamparikshadlettersetup @SamparikshadLetterSetupId,@OfficeName,@OfficeAddress,@SetupStatus,@SetupTypeId,@Message OUT",
                    SamparikshadLetterSetupIdParam, OfficeNameParam, OfficeAddressParam, SetupStatusParam, BerujuTypeIdParam, MessageParam);
                int PKID = model.ObjSamparikshadLetterOfficeSetupViewModel.SamparikshadLetterSetupId;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public string ChangeUserPassword(string Username)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                string msg = string.Empty;
                var UserNameParam = new SqlParameter { ParameterName = "@UserName", Value = Username.Trim() };
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec ChangedUserPassword @UserName,@Message OUT",
                    UserNameParam, MessageParam);

                msg = MessageParam.SqlValue.ToString();
                return msg;

            }
        }


        public List<ListUserForSMSMV> SP_GetPersonDetailsForSMS(int fiscalyearId, int officeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<ListUserForSMSMV> ReturnList = new List<ListUserForSMSMV>();
                ReturnList = db.Database.SqlQuery<ListUserForSMSMV>("SP_GetPersonDetailsForSMS {0},{1}", fiscalyearId, officeId).ToList();
                return ReturnList;
            }
        }


        public List<ListUserForSMSMV> SMS_GetMinistryLevelUsersDetails(int fiscalyearId, int berujuTypeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<ListUserForSMSMV> ReturnList = new List<ListUserForSMSMV>();
                ReturnList = db.Database.SqlQuery<ListUserForSMSMV>("SMS_GetMinistryLevelUsersDetails {0},{1}", fiscalyearId, berujuTypeId).ToList();
                return ReturnList;
            }
        }

        public string InsertSMSDetails(SMSStatus model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                string rtnMessage = string.Empty;
                db.SMSStatus.Add(model);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    rtnMessage = "Saved";
                }
                else
                {
                    rtnMessage = "Fail";
                }
                return rtnMessage;

            }
        }

        public int TotalNumberInsertedIntoSMS(int? OfficeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                int count = db.SMSStatus.Count(x => x.OfficeId == OfficeId);

                return count;
            }
        }
        public int CheckIfAlreadySentSMS(int? officeid, string mobilenumber)
        {
            DateTime checkDateCn = DateTime.Now;
            checkDateCn = checkDateCn.AddDays(15);
            using (BerujuEntities db = new BerujuEntities())
            {
                int count = db.SMSStatus.Count(x => x.OfficeId == officeid && x.MobileNumber == mobilenumber);
                return count;
            }
        }

        public int CheckIfAlreadySentSMSOld(int? officeid, string mobilenumber)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                int count = db.SMSStatus.Count(x => x.OfficeId == officeid && x.MobileNumber == mobilenumber);
                return count;
            }
        }

        public CommonViewModel GetOfficeChiefOrAuditorNameFromDate(DateTime? checkDate, int ChiefOrEditor, int officeId)
        {
            CommonViewModel model = new CommonViewModel();
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var CheckDateParam = new SqlParameter { ParameterName = "@CheckDate", Value = checkDate };
                    var ChiefOrAuditorParam = new SqlParameter { ParameterName = "@ChiefOrAuditor", Value = ChiefOrEditor };
                    var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = officeId };
                    model = db.Database.SqlQuery<CommonViewModel>("GetOfficeChiefOrAuditorNameFromDate @CheckDate,@ChiefOrAuditor,@OfficeId", CheckDateParam, ChiefOrAuditorParam, OfficeIdParam).FirstOrDefault();
                    if(model==null)
                    {
                        model = new CommonViewModel();
                    }
                }
                catch (Exception)
                {

                   model=new CommonViewModel();
                }


            }

            return model;

        }





    }
}