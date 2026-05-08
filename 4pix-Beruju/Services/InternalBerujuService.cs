using _4pix_Beruju.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace _4pix_Beruju.Services
{
    public class InternalBerujuService
    {
        public List<InternalBeruju> ListInternalBeruju(int OfficeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<InternalBeruju> ReturnList = new List<InternalBeruju>();

                ReturnList = db.Database.SqlQuery<InternalBeruju>("ListInternalBeruju {0}", OfficeId).ToList();
                return ReturnList;
            }
        }


        public List<ExternalBeruju> ListExternalBerujuTopFive(int OfficeId, int KoshTypeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<ExternalBeruju> ReturnList = new List<ExternalBeruju>();

                ReturnList = db.Database.SqlQuery<ExternalBeruju>("ListExternalBerujuTopFive {0},{1}", OfficeId, KoshTypeId).ToList();
                return ReturnList;
            }
        }

        public List<InternalBeruju> ListInternalBerujuTopFive(int OfficeId, int KoshTypeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<InternalBeruju> ReturnList = new List<InternalBeruju>();

                ReturnList = db.Database.SqlQuery<InternalBeruju>("ListInternalBerujuTopFive {0},{1}", OfficeId, KoshTypeId).ToList();
                return ReturnList;
            }
        }

        public InternalBeruju ListInternalBerujuByPrimaryId(int OfficeId, int PrimaryId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                InternalBeruju ReturnModel = new InternalBeruju();

                ReturnModel = db.Database.SqlQuery<InternalBeruju>("ListInternalBerujuByPrimaryId {0},{1}", OfficeId, PrimaryId).FirstOrDefault();
                return ReturnModel;
            }
        }
        public ReturnMessageViewModel InsertInternalBeruju(InternalBeruju model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

                if (model.KoshTypeId != 1)
                {
                    model.BudgetSubTitle = "001";//other kosh does not have budget subtitle
                }
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
                var BudgetSubTitleParam = new SqlParameter { ParameterName = "@BudgetSubTitle", Value = model.BudgetSubTitle };
                var ExpenseTItleParam = new SqlParameter { ParameterName = "@ExpenseTItle", Value = model.ExpenseTItle };



                model.OfficeManagerName = "Default";
                model.FromDate = DateTime.Now;
                model.ToDate = DateTime.Now;
                model.AccountantFromDate = DateTime.Now;
                model.AccountantToDate = DateTime.Now;
                model.AccountantName = "Default";
                model.AuditorName = "Default";
                model.AuditorPost = "Default";

                var OfficeManagerNameParam = new SqlParameter { ParameterName = "@OfficeManagerName", Value = model.OfficeManagerName };
                var OfficeManagerPostParam = new SqlParameter { ParameterName = "@OfficeManagerPost", Value = model.OfficeManagerPost };
                var FromDateParam = new SqlParameter { ParameterName = "@FromDate", Value = model.FromDate };
                var ToDateParam = new SqlParameter { ParameterName = "@ToDate", Value = model.ToDate };

                var AccountantNameParam = new SqlParameter { ParameterName = "@AccountantName", Value = model.AccountantName };
                var AccountantFromDateParam = new SqlParameter { ParameterName = "@AccountantFromDate", Value = model.AccountantFromDate };
                var AccountantToDateParam = new SqlParameter { ParameterName = "@AccountantToDate", Value = model.AccountantToDate };
                var JVNUMBERParam = new SqlParameter { ParameterName = "@JVNUMBER", Value = model.JVNUMBER };
                var VoucharDateParam = new SqlParameter { ParameterName = "@VoucharDate", Value = model.VoucharDate };
                var VoucharAmuntParam = new SqlParameter { ParameterName = "@VoucharAmunt", Value = model.VoucharAmunt };
                var BerujuDetailsParam = new SqlParameter { ParameterName = "@BerujuDetails", Value = model.BerujuDetails };
                var BerujuShorDescParam = new SqlParameter { ParameterName = "@BerujuShorDesc", Value = model.BerujuShorDesc == null ? string.Empty : model.BerujuShorDesc };
                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var ToWhomIDParam = new SqlParameter { ParameterName = "@ToWhomID", Value = model.ToWhomID };
                var ToWhomNameParam = new SqlParameter { ParameterName = "@ToWhomName", Value = model.ToWhomName };

                var AuditorNameParam = new SqlParameter { ParameterName = "@AuditorName", Value = model.AuditorName };
                var AuditorPostParam = new SqlParameter { ParameterName = "@AuditorPost", Value = model.AuditorPost };
                var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = model.KoshTypeId };
                var BerujuNumberParam = new SqlParameter { ParameterName = "@BerujuNumber", Value = model.BerujuNumber };
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = model.BerujuStatus };
                var CreatedByParam = new SqlParameter { ParameterName = "@CreatedBy", Value = model.CreatedBy };
                var WasMadeFinalParam = new SqlParameter { ParameterName = "@WasMadeFinal", Value = model.WasMadeFinal };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };

                int ManagerId = GetEmployeeOrEditorId(model.OfficeId, 1, model.VoucharDate);
                int AccountantId = GetEmployeeOrEditorId(model.OfficeId, 2, model.VoucharDate);
                model.AuditorId = GetEmployeeOrEditorId(model.OfficeId, 3, model.VoucharDate);

                var OfficeManagerIdParam = new SqlParameter { ParameterName = "@OfficeManagerId", Value = ManagerId };
                var AccountantIdParam = new SqlParameter { ParameterName = "@AccountantId", Value = AccountantId };
                var AuditorIdParam = new SqlParameter { ParameterName = "@AuditorId", Value = model.AuditorId.HasValue ? model.AuditorId : 0 };
                var BerujuSubTitleIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleId", Value = model.BerujuSubTitleId.HasValue ? model.BerujuSubTitleId : 0 };
                var ChaluOrPujigatParam = new SqlParameter { ParameterName = "@ChaluOrPujigat", Value = model.ChaluOrPujigatId.HasValue ? model.ChaluOrPujigatId : 0 };
                var ChaluOrPujigatTitleIdParam = new SqlParameter { ParameterName = "@ChaluOrPujigatTitleId", Value = model.KoshTypeTitleListId.HasValue ? model.KoshTypeTitleListId : 0 };
                var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = model.BerujuAmount.HasValue ? model.BerujuAmount : 0 };
                var IsSaidantikBerujuParam = new SqlParameter { ParameterName = "@IsSaidantikBeruju", Value = model.IsSaidantikBeruju };
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

                var result = db.Database.ExecuteSqlCommand("exec InsertInternalBeruju @FiscalYearId,@BudgetSubTitle,@ExpenseTItle,@OfficeManagerName,@OfficeManagerPost,@FromDate,@ToDate,@AccountantName,@AccountantFromDate,@AccountantToDate,@JVNUMBER,@VoucharDate,@VoucharAmunt,@BerujuDetails,@BerujuShorDesc,@BerujuTypeId,@ToWhomID,@ToWhomName,@AuditorName,@AuditorPost,@KoshTypeId,@BerujuNumber,@BerujuStatus,@CreatedBy,@WasMadeFinal,@OfficeId,@OfficeManagerId,@AccountantId,@AuditorId,@BerujuSubTitleId,@ChaluOrPujigat,@ChaluOrPujigatTitleId,@BerujuAmount,@IsSaidantikBeruju,@Message OUT,@PrimaryId OUT",
                    FiscalYearIdParam, BudgetSubTitleParam, ExpenseTItleParam, OfficeManagerNameParam, OfficeManagerPostParam, FromDateParam, ToDateParam, AccountantNameParam, AccountantFromDateParam, AccountantToDateParam, JVNUMBERParam, VoucharDateParam, VoucharAmuntParam, BerujuDetailsParam, BerujuShorDescParam, BerujuTypeIdParam, ToWhomIDParam, ToWhomNameParam, AuditorNameParam, AuditorPostParam, KoshTypeIdParam, BerujuNumberParam, BerujuStatusParam, CreatedByParam, WasMadeFinalParam, OfficeIdParam, OfficeManagerIdParam, AccountantIdParam, AuditorIdParam, BerujuSubTitleIdParam, ChaluOrPujigatParam, ChaluOrPujigatTitleIdParam, BerujuAmountParam, IsSaidantikBerujuParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                if (PKID > 0)
                {
                    if (model.ToWhomDetailListVMList.Count > 0)
                    {


                        foreach (var item in model.ToWhomDetailListVMList)
                        {
                            var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = 1 };
                            var InternalOrExternalIdParam = new SqlParameter { ParameterName = "@InternalOrExternalId", Value = PKID };
                            var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.EmployeeName == null ? string.Empty : item.EmployeeName };
                            var TotalAmountParam = new SqlParameter { ParameterName = "@TotalAmount", Value = item.AmountDetail.HasValue ? item.AmountDetail : 0 };
                            var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                            var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };


                            var MessageParamToWhom = new SqlParameter
                            {
                                ParameterName = "@MessageToWhom",
                                DbType = DbType.String,
                                Size = 50,
                                Direction = System.Data.ParameterDirection.Output
                            };

                            var resultToWhom = db.Database.ExecuteSqlCommand("exec InsertToWhomDetails @InternalOrExternal,@InternalOrExternalId,@PersonName,@TotalAmount,@PanNumber,@MobielNumber,@MessageToWhom OUT", InternalOrExternalParam, InternalOrExternalIdParam, PersonNameParam, TotalAmountParam, PanNumberParam, MobielNumberParam, MessageParamToWhom);



                        }
                    }
                }


                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel UpdateInternalBeruju(InternalBeruju model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                //Default Values
                if (model.KoshTypeId != 1)
                {
                    model.BudgetSubTitle = "001";//other kosh does not have budget subtitle
                }
                model.OfficeManagerName = "Default";
                model.FromDate = DateTime.Now;
                model.ToDate = DateTime.Now;
                model.AccountantFromDate = DateTime.Now;
                model.AccountantToDate = DateTime.Now;
                model.AccountantName = "Default";

                model.AuditorName = "Default";
                model.AuditorPost = "Default";

                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var InternalBerujuIdParam = new SqlParameter { ParameterName = "@InternalBerujuId", Value = model.InternalBerujuId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
                var BudgetSubTitleParam = new SqlParameter { ParameterName = "@BudgetSubTitle", Value = model.BudgetSubTitle };
                var ExpenseTItleParam = new SqlParameter { ParameterName = "@ExpenseTItle", Value = model.ExpenseTItle };
                var OfficeManagerNameParam = new SqlParameter { ParameterName = "@OfficeManagerName", Value = model.OfficeManagerName };
                var OfficeManagerPostParam = new SqlParameter { ParameterName = "@OfficeManagerPost", Value = model.OfficeManagerPost };
                var FromDateParam = new SqlParameter { ParameterName = "@FromDate", Value = model.FromDate };
                var ToDateParam = new SqlParameter { ParameterName = "@ToDate", Value = model.ToDate };
                var AccountantNameParam = new SqlParameter { ParameterName = "@AccountantName", Value = model.AccountantName };

                var AccountantFromDateParam = new SqlParameter { ParameterName = "@AccountantFromDate", Value = model.AccountantFromDate };
                var AccountantToDateParam = new SqlParameter { ParameterName = "@AccountantToDate", Value = model.AccountantToDate };
                var JVNUMBERParam = new SqlParameter { ParameterName = "@JVNUMBER", Value = model.JVNUMBER };
                var VoucharDateParam = new SqlParameter { ParameterName = "@VoucharDate", Value = model.VoucharDate };
                var VoucharAmuntParam = new SqlParameter { ParameterName = "@VoucharAmunt", Value = model.VoucharAmunt };
                var BerujuDetailsParam = new SqlParameter { ParameterName = "@BerujuDetails", Value = model.BerujuDetails };
                var BerujuShorDescParam = new SqlParameter { ParameterName = "@BerujuShorDesc", Value = model.BerujuShorDesc == null ? string.Empty : model.BerujuShorDesc };

                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var ToWhomIDParam = new SqlParameter { ParameterName = "@ToWhomID", Value = model.ToWhomID };
                var ToWhomNameParam = new SqlParameter { ParameterName = "@ToWhomName", Value = model.ToWhomName };

                var AuditorNameParam = new SqlParameter { ParameterName = "@AuditorName", Value = model.AuditorName };
                var AuditorPostParam = new SqlParameter { ParameterName = "@AuditorPost", Value = model.AuditorPost };
                var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = model.KoshTypeId };
                var BerujuNumberParam = new SqlParameter { ParameterName = "@BerujuNumber", Value = model.BerujuNumber };
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = model.BerujuStatus };
                var UpdatedByParam = new SqlParameter { ParameterName = "@UpdatedBy", Value = model.CreatedBy };
                var WasMadeFinalParam = new SqlParameter { ParameterName = "@WasMadeFinal", Value = model.WasMadeFinal };

                int ManagerId = GetEmployeeOrEditorId(model.OfficeId, 1, model.VoucharDate);
                int AccountantId = GetEmployeeOrEditorId(model.OfficeId, 2, model.VoucharDate);
                model.AuditorId = GetEmployeeOrEditorId(model.OfficeId, 3, model.VoucharDate);
                var OfficeManagerIdParam = new SqlParameter { ParameterName = "@OfficeManagerId", Value = ManagerId };
                var AccountantIdParam = new SqlParameter { ParameterName = "@AccountantId", Value = AccountantId };
                var AuditorIdParam = new SqlParameter { ParameterName = "@AuditorId", Value = model.AuditorId.HasValue ? model.AuditorId : 0 };
                var BerujuSubTitleIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleId", Value = model.BerujuSubTitleId.HasValue ? model.BerujuSubTitleId : 0 };
                var ChaluOrPujigatParam = new SqlParameter { ParameterName = "@ChaluOrPujigat", Value = model.ChaluOrPujigatId.HasValue ? model.ChaluOrPujigatId : 0 };
                var ChaluOrPujigatTitleIdParam = new SqlParameter { ParameterName = "@ChaluOrPujigatTitleId", Value = model.KoshTypeTitleListId.HasValue ? model.KoshTypeTitleListId : 0 };
                var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = model.BerujuAmount.HasValue ? model.BerujuAmount : 0 };
                var IsSaidantikBerujuParam = new SqlParameter { ParameterName = "@IsSaidantikBeruju", Value = model.IsSaidantikBeruju };

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

                var result = db.Database.ExecuteSqlCommand("exec UpdateInternalBeruju @InternalBerujuId,@FiscalYearId,@BudgetSubTitle,@ExpenseTItle,@OfficeManagerName,@OfficeManagerPost,@FromDate,@ToDate,@AccountantName,@AccountantFromDate,@AccountantToDate,@JVNUMBER,@VoucharDate,@VoucharAmunt,@BerujuDetails,@BerujuShorDesc,@BerujuTypeId,@ToWhomID,@ToWhomName,@AuditorName,@AuditorPost,@KoshTypeId,@BerujuNumber,@BerujuStatus,@WasMadeFinal,@UpdatedBy,@OfficeManagerId,@AccountantId,@AuditorId,@BerujuSubTitleId,@ChaluOrPujigat,@ChaluOrPujigatTitleId,@BerujuAmount,@IsSaidantikBeruju,@Message OUT,@PrimaryId OUT",
                    InternalBerujuIdParam, FiscalYearIdParam, BudgetSubTitleParam, ExpenseTItleParam, OfficeManagerNameParam, OfficeManagerPostParam, FromDateParam, ToDateParam, AccountantNameParam, AccountantFromDateParam, AccountantToDateParam, JVNUMBERParam, VoucharDateParam, VoucharAmuntParam, BerujuDetailsParam, BerujuShorDescParam, BerujuTypeIdParam, ToWhomIDParam, ToWhomNameParam, AuditorNameParam, AuditorPostParam, KoshTypeIdParam, BerujuNumberParam, BerujuStatusParam, WasMadeFinalParam, UpdatedByParam, OfficeManagerIdParam, AccountantIdParam, AuditorIdParam, BerujuSubTitleIdParam, ChaluOrPujigatParam, ChaluOrPujigatTitleIdParam, BerujuAmountParam, IsSaidantikBerujuParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                if (returnModel.ReturnMessage == "Updated Successfully")
                {

                    if (model.ToWhomDetailListVMList.Count > 0)
                    {
                        var InternalOrExternalBerujuIdForDeleteParam = new SqlParameter { ParameterName = "@InternalOrExternalBerujuIdForDelete", Value = PKID };
                        var resultDel = db.Database.ExecuteSqlCommand("exec DeleteToWhomDetailsByBerujuId @InternalOrExternalBerujuIdForDelete", InternalOrExternalBerujuIdForDeleteParam);



                        foreach (var item in model.ToWhomDetailListVMList)
                        {
                            var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = 1 };
                            var InternalOrExternalIdParam = new SqlParameter { ParameterName = "@InternalOrExternalId", Value = PKID };
                            var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.EmployeeName == null ? string.Empty : item.EmployeeName };
                            var TotalAmountParam = new SqlParameter { ParameterName = "@TotalAmount", Value = item.AmountDetail.HasValue ? item.AmountDetail : 0 };
                            var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                            var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };


                            var MessageParamToWhom = new SqlParameter
                            {
                                ParameterName = "@MessageToWhom",
                                DbType = DbType.String,
                                Size = 50,
                                Direction = System.Data.ParameterDirection.Output
                            };

                            var resultToWhom = db.Database.ExecuteSqlCommand("exec InsertToWhomDetails @InternalOrExternal,@InternalOrExternalId,@PersonName,@TotalAmount,@PanNumber,@MobielNumber,@MessageToWhom OUT", InternalOrExternalParam, InternalOrExternalIdParam, PersonNameParam, TotalAmountParam, PanNumberParam, MobielNumberParam, MessageParamToWhom);



                        }
                    }


                }


                return returnModel;
            }

        }

        public List<ToWhomDetailListVM> ListTowhomDetails(int PrimaryId, int InternalOrExternal)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<ToWhomDetailListVM> ReturnList = new List<ToWhomDetailListVM>();

                ReturnList = db.Database.SqlQuery<ToWhomDetailListVM>("GetToWhomDetailsByBerujuId {0},{1}", PrimaryId, InternalOrExternal).ToList();
                return ReturnList;
            }
        }

        public ReturnMessageViewModel ChangeInternalBerujuToFinalBeruju(int InternalBerujuId)
        {
            ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
            using (BerujuEntities db = new BerujuEntities())
            {
                var InternalBerujuIdParam = new SqlParameter { ParameterName = "@InternalBerujuId", Value = InternalBerujuId };
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

                var result = db.Database.ExecuteSqlCommand("exec ChangeInternalBerujuToFinalBeruju @InternalBerujuId,@Message OUT,@PrimaryId OUT",
                        InternalBerujuIdParam, MessageParam, PrimaryIdParam);

                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel DeleteInternalBeruju(int InternalBerujuId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var InternalBerujuIdParam = new SqlParameter { ParameterName = "@InternalBerujuId", Value = InternalBerujuId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec DeleteInternalBeruju @InternalBerujuId,@Message OUT", InternalBerujuIdParam, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = 0;
                return returnModel;
            }

        }


        public ReturnMessageViewModel DeleteExternalBeruju(int InternalBerujuId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = InternalBerujuId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec DeleteExternalBeruju @ExternalBerujuId,@Message OUT", ExternalBerujuIdParam, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = 0;
                return returnModel;
            }

        }


        public ReturnMessageViewModel DeleteExternalBerujuFromChecker(int InternalBerujuId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = InternalBerujuId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec DeleteExternalBerujuFromChecker @ExternalBerujuId,@Message OUT", ExternalBerujuIdParam, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = 0;
                return returnModel;
            }

        }




        #region External Beruju

        public List<ExternalBeruju> ListMakerEntryBeruju(bool BerujuStatus)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<ExternalBeruju> ReturnList = new List<ExternalBeruju>();

                ReturnList = db.Database.SqlQuery<ExternalBeruju>("ListMakerEntryBeruju {0}", BerujuStatus).ToList();
                return ReturnList;
            }
        }

        public List<SaidantikBeruju> ListMakerSaidantikEntryBeruju(bool BerujuStatus)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<SaidantikBeruju> ReturnList = new List<SaidantikBeruju>();

                ReturnList = db.Database.SqlQuery<SaidantikBeruju>("ListMakerSaidantikEntryBeruju {0}", BerujuStatus).ToList();
                return ReturnList;
            }
        }


        public List<OfficeBerujuDTO> GetOfficeWiseBeruju(BerujuCheckerReportFilter filter)
        {
            using (var ent = new BerujuEntities())
            {
                return ent.Database.SqlQuery<OfficeBerujuDTO>(
                    "EXEC sp_GetOfficeWiseBeruju @FromDate, @ToDate, @OfficeId, @FiscalYearId, @BerujuStatus",
                    new SqlParameter("@FromDate", (object)filter.FromDate ?? DBNull.Value),
                    new SqlParameter("@ToDate", (object)filter.ToDate ?? DBNull.Value),
                    new SqlParameter("@OfficeId", (object)filter.OfficeId ?? DBNull.Value),
                    new SqlParameter("@FiscalYearId", (object)filter.FiscalYearId ?? DBNull.Value),
                    new SqlParameter("@BerujuStatus", filter.BerujuStatus)
                ).ToList();
            }
        }

        public List<OfficeBerujuDTO> GetOfficeWiseSadantikBeruju(BerujuCheckerReportFilter filter)
        {
            using (var ent = new BerujuEntities())
            {
                return ent.Database.SqlQuery<OfficeBerujuDTO>(
                    "EXEC sp_GetOfficeWiseSadantikBeruju @FromDate, @ToDate, @OfficeId, @FiscalYearId, @BerujuStatus",
                    new SqlParameter("@FromDate", (object)filter.FromDate ?? DBNull.Value),
                    new SqlParameter("@ToDate", (object)filter.ToDate ?? DBNull.Value),
                    new SqlParameter("@OfficeId", (object)filter.OfficeId ?? DBNull.Value),
                    new SqlParameter("@FiscalYearId", (object)filter.FiscalYearId ?? DBNull.Value),
                    new SqlParameter("@BerujuStatus", filter.BerujuStatus)
                ).ToList();
            }
        }

        public List<ExternalBeruju> ListExternalBeruju(int OfficeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<ExternalBeruju> ReturnList = new List<ExternalBeruju>();

                ReturnList = db.Database.SqlQuery<ExternalBeruju>("ListExternalBeruju {0}", OfficeId).ToList();
                return ReturnList;
            }
        }

        public List<ExternalBeruju> SPListExternalBerujuByKoshTypeId(int OfficeId, int KoshTypeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<ExternalBeruju> ReturnList = new List<ExternalBeruju>();

                ReturnList = db.Database.SqlQuery<ExternalBeruju>("SPListExternalBerujuByKoshTypeId {0},{1}", OfficeId, KoshTypeId).ToList();
                return ReturnList;
            }
        }


        public List<ExternalBeruju> ListExternalBerujuForSamparikshadMake(int OfficeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<ExternalBeruju> ReturnList = new List<ExternalBeruju>();

                ReturnList = db.Database.SqlQuery<ExternalBeruju>("GetExternalBerujulistForSamparikshadMake {0}", OfficeId).ToList();
                return ReturnList;
            }
        }

        public List<InternalBeruju> IN_GetInternalBerujulistForSamparikshadMake(int OfficeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<InternalBeruju> ReturnList = new List<InternalBeruju>();

                ReturnList = db.Database.SqlQuery<InternalBeruju>("IN_GetInternalBerujulistForSamparikshadMake {0}", OfficeId).ToList();
                return ReturnList;
            }
        }


        public List<ExternalBeruju> ListExternalBerujuForSamparikshadRequestMake(int OfficeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<ExternalBeruju> ReturnList = new List<ExternalBeruju>();

                ReturnList = db.Database.SqlQuery<ExternalBeruju>("GetExternalBerujulistForSamparikshadRequestMake {0}", OfficeId).ToList();
                return ReturnList;
            }
        }
        //update from sep1 2023
        public List<ExternalBeruju> SPB_GetListForRequestMake(int OfficeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<ExternalBeruju> ReturnList = new List<ExternalBeruju>();

                ReturnList = db.Database.SqlQuery<ExternalBeruju>("SPB_GetListForRequestMake {0}", OfficeId).ToList();
                return ReturnList;
            }
        }







        public ExternalBeruju ListExternalBerujuByPrimaryId(int OfficeId, int PrimaryId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ExternalBeruju ReturnModel = new ExternalBeruju();

                ReturnModel = db.Database.SqlQuery<ExternalBeruju>("ListExterBerujuByPrimaryId {0},{1}", OfficeId, PrimaryId).FirstOrDefault();
                return ReturnModel;
            }
        }

        public InternalBerujuForSamparikshadVM IN_GetInternalBerujuDetailForSamparikshad(int PrimaryId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                InternalBerujuForSamparikshadVM ReturnModel = new InternalBerujuForSamparikshadVM();

                ReturnModel = db.Database.SqlQuery<InternalBerujuForSamparikshadVM>("IN_GetInternalBerujuDetailForSamparikshad {0}", PrimaryId).FirstOrDefault();
                return ReturnModel;
            }
        }



        public ExternalBerujuForSamparikshadVM GetExternalBerujuDetailForSamparikshad(int PrimaryId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ExternalBerujuForSamparikshadVM ReturnModel = new ExternalBerujuForSamparikshadVM();

                ReturnModel = db.Database.SqlQuery<ExternalBerujuForSamparikshadVM>("GetExternalBerujuDetailForSamparikshad {0}", PrimaryId).FirstOrDefault();
                return ReturnModel;
            }
        }

        public int GetEmployeeOrEditorId(int OfficeId, int Type, DateTime VoucherDate)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                int ReturnId = 0;
                ReturnId = db.Database.SqlQuery<int>("GetEmployeeOrAuditorIdForBeruju {0},{1},{2}", OfficeId, Type, VoucherDate).FirstOrDefault();
                return ReturnId;
            }
        }

        public ManagerOrAuditorNameViewModel GetEmployeeOrAuditorByPrimaryId(int OfficeId, int? EmpId)
        {
            ManagerOrAuditorNameViewModel returnModel = new ManagerOrAuditorNameViewModel();
            using (BerujuEntities db = new BerujuEntities())
            {

                returnModel = db.Database.SqlQuery<ManagerOrAuditorNameViewModel>("GetEmployeeOrAuditorByPrimaryId {0},{1}", OfficeId, EmpId).FirstOrDefault();
                if (returnModel == null)
                {
                    returnModel = new ManagerOrAuditorNameViewModel();
                }
                return returnModel;
            }
        }


        #endregion

        public ReturnMessageViewModel InsertExternalBeruju(ExternalBeruju model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

                if (model.KoshTypeId != 1)
                {
                    model.BudgetSubTitle = "001";//other kosh does not have budget subtitle
                }
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
                var BudgetSubTitleParam = new SqlParameter { ParameterName = "@BudgetSubTitle", Value = model.BudgetSubTitle };
                var ExpenseTItleParam = new SqlParameter { ParameterName = "@ExpenseTItle", Value = model.ExpenseTItle };

                model.OfficeManagerName = "Default";
                model.FromDate = DateTime.Now;
                model.ToDate = DateTime.Now;
                model.AccountantFromDate = DateTime.Now;
                model.AccountantToDate = DateTime.Now;
                model.BerujuStatus = false;
                model.AccountantName = "Default";
                model.AuditorName = "Default";
                model.AuditorPost = "Default";
                var UploadFileUrlParam = new SqlParameter { ParameterName = "@UploadedFileUrl", Value = model.UploadedFileUrl };
                var OfficeManagerNameParam = new SqlParameter { ParameterName = "@OfficeManagerName", Value = model.OfficeManagerName };
                var OfficeManagerPostParam = new SqlParameter { ParameterName = "@OfficeManagerPost", Value = model.OfficeManagerPost };
                var FromDateParam = new SqlParameter { ParameterName = "@FromDate", Value = model.FromDate };
                var ToDateParam = new SqlParameter { ParameterName = "@ToDate", Value = model.ToDate };

                var AccountantNameParam = new SqlParameter { ParameterName = "@AccountantName", Value = model.AccountantName };
                var AccountantFromDateParam = new SqlParameter { ParameterName = "@AccountantFromDate", Value = model.AccountantFromDate };
                var AccountantToDateParam = new SqlParameter { ParameterName = "@AccountantToDate", Value = model.AccountantToDate };
                var JVNUMBERParam = new SqlParameter { ParameterName = "@JVNUMBER", Value = model.JVNUMBER };
                var VoucharDateParam = new SqlParameter { ParameterName = "@VoucharDate", Value = model.VoucharDate };
                var VoucharAmuntParam = new SqlParameter { ParameterName = "@VoucharAmunt", Value = model.VoucharAmunt };
                var BerujuDetailsParam = new SqlParameter { ParameterName = "@BerujuDetails", Value = model.BerujuDetails };
                var BerujuShorDescParam = new SqlParameter { ParameterName = "@BerujuShorDesc", Value = model.BerujuShorDesc == null ? string.Empty : model.BerujuShorDesc };

                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var ToWhomIDParam = new SqlParameter { ParameterName = "@ToWhomID", Value = model.ToWhomID };
                var ToWhomNameParam = new SqlParameter { ParameterName = "@ToWhomName", Value = model.ToWhomName };

                var AuditorNameParam = new SqlParameter { ParameterName = "@AuditorName", Value = model.AuditorName };
                var AuditorPostParam = new SqlParameter { ParameterName = "@AuditorPost", Value = model.AuditorPost };
                var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = model.KoshTypeId };
                var BerujuNumberParam = new SqlParameter { ParameterName = "@BerujuNumber", Value = model.BerujuNumber };
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = model.BerujuStatus };
                var CreatedByParam = new SqlParameter { ParameterName = "@CreatedBy", Value = model.CreatedBy };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };

                int ManagerId = GetEmployeeOrEditorId(model.OfficeId, 1, model.VoucharDate);
                int AccountantId = GetEmployeeOrEditorId(model.OfficeId, 2, model.VoucharDate);
                model.AuditorId = GetEmployeeOrEditorId(model.OfficeId, 3, model.VoucharDate);

                var OfficeManagerIdParam = new SqlParameter { ParameterName = "@OfficeManagerId", Value = ManagerId };
                var AccountantIdParam = new SqlParameter { ParameterName = "@AccountantId", Value = AccountantId };
                var AuditorIdParam = new SqlParameter { ParameterName = "@AuditorId", Value = model.AuditorId.HasValue ? model.AuditorId : 0 };
                var BerujuSubTitleIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleId", Value = model.BerujuSubTitleId.HasValue ? model.BerujuSubTitleId : 0 };

                var BerujuSubTitleChildIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleChildId", Value = model.BerujuSubTitleChildId.HasValue ? model.BerujuSubTitleChildId : 0 };

                var ChaluOrPujigatParam = new SqlParameter { ParameterName = "@ChaluOrPujigat", Value = model.ChaluOrPujigatId.HasValue ? model.ChaluOrPujigatId : 0 };
                var ChaluOrPujigatTitleIdParam = new SqlParameter { ParameterName = "@ChaluOrPujigatTitleId", Value = model.KoshTypeTitleListId.HasValue ? model.KoshTypeTitleListId : 0 };

                var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = model.BerujuAmount.HasValue ? model.BerujuAmount : 0 };
                var IsSaidantikBerujuParam = new SqlParameter { ParameterName = "@IsSaidantikBeruju", Value = model.IsSaidantikBeruju };

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

                var result = db.Database.ExecuteSqlCommand("exec InsertExternalBeruju @UploadedFileUrl,@FiscalYearId," +
                    "@BudgetSubTitle,@ExpenseTItle,@OfficeManagerName,@OfficeManagerPost,@FromDate,@ToDate,@AccountantName," +
                    "@AccountantFromDate,@AccountantToDate,@JVNUMBER,@VoucharDate,@VoucharAmunt,@BerujuDetails,@BerujuShorDesc," +
                    "@BerujuTypeId,@ToWhomID,@ToWhomName,@AuditorName,@AuditorPost,@KoshTypeId,@BerujuNumber,@BerujuStatus," +
                    "@CreatedBy,@OfficeId,@OfficeManagerId,@AccountantId,@AuditorId,@BerujuSubTitleId,@BerujuSubTitleChildId,@ChaluOrPujigat," +
                    "@ChaluOrPujigatTitleId,@BerujuAmount,@IsSaidantikBeruju,@Message OUT,@PrimaryId OUT",
                    UploadFileUrlParam,FiscalYearIdParam, BudgetSubTitleParam, ExpenseTItleParam, OfficeManagerNameParam, 
                    OfficeManagerPostParam, FromDateParam, ToDateParam, AccountantNameParam, AccountantFromDateParam,
                    AccountantToDateParam, JVNUMBERParam, VoucharDateParam, VoucharAmuntParam, BerujuDetailsParam, 
                    BerujuShorDescParam, BerujuTypeIdParam, ToWhomIDParam, ToWhomNameParam, AuditorNameParam, AuditorPostParam, 
                    KoshTypeIdParam, BerujuNumberParam, BerujuStatusParam, CreatedByParam, OfficeIdParam, OfficeManagerIdParam, 
                    AccountantIdParam, AuditorIdParam, BerujuSubTitleIdParam, BerujuSubTitleChildIdParam, ChaluOrPujigatParam, ChaluOrPujigatTitleIdParam, 
                    BerujuAmountParam, IsSaidantikBerujuParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;

                if (PKID > 0)
                {
                    if (model.ToWhomDetailListVMList.Count > 0)
                    {


                        foreach (var item in model.ToWhomDetailListVMList)
                        {
                            var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = 2 };
                            var InternalOrExternalIdParam = new SqlParameter { ParameterName = "@InternalOrExternalId", Value = PKID };
                            var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.EmployeeName == null ? string.Empty : item.EmployeeName };
                            var TotalAmountParam = new SqlParameter { ParameterName = "@TotalAmount", Value = item.AmountDetail.HasValue ? item.AmountDetail : 0 };
                            var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                            var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };
                            var VoucherNumberToWhomParam = new SqlParameter { ParameterName = "@VoucherNumberToWhom", Value = item.VoucherNumber == null ? string.Empty : item.VoucherNumber };
                            var VoucherDateToWhomParam = new SqlParameter { ParameterName = "@VoucherDateToWhom", Value = item.VoucherDate == null ? string.Empty : item.VoucherDate };


                            var MessageParamToWhom = new SqlParameter
                            {
                                ParameterName = "@MessageToWhom",
                                DbType = DbType.String,
                                Size = 50,
                                Direction = System.Data.ParameterDirection.Output
                            };

                            var resultToWhom = db.Database.ExecuteSqlCommand("exec InsertToWhomDetails @InternalOrExternal,@InternalOrExternalId,@PersonName,@TotalAmount,@PanNumber,@MobielNumber,@VoucherNumberToWhom,@VoucherDateToWhom,@MessageToWhom OUT", InternalOrExternalParam, InternalOrExternalIdParam, PersonNameParam, TotalAmountParam, PanNumberParam, MobielNumberParam,VoucherNumberToWhomParam,VoucherDateToWhomParam, MessageParamToWhom);



                        }
                    }
                }


                return returnModel;
            }

        }


        public ReturnMessageViewModel UpdateExternalBeruju(ExternalBeruju model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

                if (model.KoshTypeId != 1)
                {
                    model.BudgetSubTitle = "001";//other kosh does not have budget subtitle
                }
                var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = model.ExternalBerujuId };

                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
                var BudgetSubTitleParam = new SqlParameter { ParameterName = "@BudgetSubTitle", Value = model.BudgetSubTitle };
                var ExpenseTItleParam = new SqlParameter { ParameterName = "@ExpenseTItle", Value = model.ExpenseTItle };

                model.BerujuStatus = false;
                model.OfficeManagerName = "Default";
                model.FromDate = DateTime.Now;
                model.ToDate = DateTime.Now;
                model.AccountantFromDate = DateTime.Now;
                model.AccountantToDate = DateTime.Now;
                model.AccountantName = "Default";
                model.AuditorName = "Default";
                model.AuditorPost = "Default";

                var UploadFileUrlParam = new SqlParameter { ParameterName = "@UploadedFileUrl", Value = model.UploadedFileUrl };
                var OfficeManagerNameParam = new SqlParameter { ParameterName = "@OfficeManagerName", Value = model.OfficeManagerName };
                var OfficeManagerPostParam = new SqlParameter { ParameterName = "@OfficeManagerPost", Value = model.OfficeManagerPost };
                var FromDateParam = new SqlParameter { ParameterName = "@FromDate", Value = model.FromDate };
                var ToDateParam = new SqlParameter { ParameterName = "@ToDate", Value = model.ToDate };

                var AccountantNameParam = new SqlParameter { ParameterName = "@AccountantName", Value = model.AccountantName };
                var AccountantFromDateParam = new SqlParameter { ParameterName = "@AccountantFromDate", Value = model.AccountantFromDate };
                var AccountantToDateParam = new SqlParameter { ParameterName = "@AccountantToDate", Value = model.AccountantToDate };
                var JVNUMBERParam = new SqlParameter { ParameterName = "@JVNUMBER", Value = model.JVNUMBER };
                var VoucharDateParam = new SqlParameter { ParameterName = "@VoucharDate", Value = model.VoucharDate };
                var VoucharAmuntParam = new SqlParameter { ParameterName = "@VoucharAmunt", Value = model.VoucharAmunt };
                var BerujuDetailsParam = new SqlParameter { ParameterName = "@BerujuDetails", Value = model.BerujuDetails };
                var BerujuShorDescParam = new SqlParameter { ParameterName = "@BerujuShorDesc", Value = model.BerujuShorDesc == null ? string.Empty : model.BerujuShorDesc };

                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var ToWhomIDParam = new SqlParameter { ParameterName = "@ToWhomID", Value = model.ToWhomID };
                var ToWhomNameParam = new SqlParameter { ParameterName = "@ToWhomName", Value = model.ToWhomName };

                var AuditorNameParam = new SqlParameter { ParameterName = "@AuditorName", Value = model.AuditorName };
                var AuditorPostParam = new SqlParameter { ParameterName = "@AuditorPost", Value = model.AuditorPost };
                var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = model.KoshTypeId };
                var BerujuNumberParam = new SqlParameter { ParameterName = "@BerujuNumber", Value = model.BerujuNumber };
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = model.BerujuStatus };
                var InternalBerujuIdParam = new SqlParameter { ParameterName = "@InternalBerujuId", Value = model.InternalBerujuId };
                var UdpatedByParam = new SqlParameter { ParameterName = "@UpdatedBy", Value = model.CreatedBy };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };

                int ManagerId = GetEmployeeOrEditorId(model.OfficeId, 1, model.VoucharDate);
                int AccountantId = GetEmployeeOrEditorId(model.OfficeId, 2, model.VoucharDate);
                model.AuditorId = GetEmployeeOrEditorId(model.OfficeId, 3, model.VoucharDate);
                var OfficeManagerIdParam = new SqlParameter { ParameterName = "@OfficeManagerId", Value = ManagerId };
                var AccountantIdParam = new SqlParameter { ParameterName = "@AccountantId", Value = AccountantId };
                var AuditorIdParam = new SqlParameter { ParameterName = "@AuditorId", Value = model.AuditorId.HasValue ? model.AuditorId : 0 };
                var BerujuSubTitleIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleId", Value = model.BerujuSubTitleId.HasValue ? model.BerujuSubTitleId : 0 };
                var BerujuSubTitleChildIdParam = new SqlParameter { ParameterName = "@BerujuSubTitleChildId", Value = model.BerujuSubTitleChildId.HasValue ? model.BerujuSubTitleChildId : 0 };


                var ChaluOrPujigatParam = new SqlParameter { ParameterName = "@ChaluOrPujigat", Value = model.ChaluOrPujigatId.HasValue ? model.ChaluOrPujigatId : 0 };
                var ChaluOrPujigatTitleIdParam = new SqlParameter { ParameterName = "@ChaluOrPujigatTitleId", Value = model.KoshTypeTitleListId.HasValue ? model.KoshTypeTitleListId : 0 };
                var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = model.BerujuAmount.HasValue ? model.BerujuAmount : 0 };
                var IsSaidantikBerujuParam = new SqlParameter { ParameterName = "@IsSaidantikBeruju", Value = model.IsSaidantikBeruju };


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

                var result = db.Database.ExecuteSqlCommand("exec UpdateExternalBeruju @UploadedFileUrl,@ExternalBerujuId,@FiscalYearId,@BudgetSubTitle,@ExpenseTItle," +
                    "@OfficeManagerName,@OfficeManagerPost,@FromDate,@ToDate,@AccountantName,@AccountantFromDate,@AccountantToDate,@JVNUMBER,@VoucharDate,@VoucharAmunt," +
                    "@BerujuDetails,@BerujuShorDesc,@BerujuTypeId,@ToWhomID,@ToWhomName,@AuditorName,@AuditorPost,@KoshTypeId,@BerujuNumber,@BerujuStatus,@InternalBerujuId," +
                    "@UpdatedBy,@OfficeId,@OfficeManagerId,@AccountantId,@AuditorId,@BerujuSubTitleId,@BerujuSubTitleChildId,@ChaluOrPujigat,@ChaluOrPujigatTitleId,@BerujuAmount,@IsSaidantikBeruju," +
                    "@Message OUT,@PrimaryId OUT",
                    UploadFileUrlParam,ExternalBerujuIdParam, FiscalYearIdParam, BudgetSubTitleParam, ExpenseTItleParam, OfficeManagerNameParam, 
                    OfficeManagerPostParam, FromDateParam, ToDateParam, AccountantNameParam, AccountantFromDateParam, AccountantToDateParam, JVNUMBERParam, 
                    VoucharDateParam, VoucharAmuntParam, BerujuDetailsParam, BerujuShorDescParam, BerujuTypeIdParam, ToWhomIDParam, ToWhomNameParam, AuditorNameParam,
                    AuditorPostParam, KoshTypeIdParam, BerujuNumberParam, BerujuStatusParam, InternalBerujuIdParam, UdpatedByParam, OfficeIdParam, OfficeManagerIdParam, 
                    AccountantIdParam, AuditorIdParam, BerujuSubTitleIdParam, BerujuSubTitleChildIdParam, ChaluOrPujigatParam, ChaluOrPujigatTitleIdParam, BerujuAmountParam, IsSaidantikBerujuParam, 
                    MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;

                if (returnModel.ReturnMessage == "Updated Successfully")
                {

                    if (model.ToWhomDetailListVMList.Count > 0)
                    {
                        var InternalOrExternalBerujuIdForDeleteParam = new SqlParameter { ParameterName = "@InternalOrExternalBerujuIdForDelete", Value = PKID };
                        var resultDel = db.Database.ExecuteSqlCommand("exec DeleteToWhomDetailsByBerujuId @InternalOrExternalBerujuIdForDelete", InternalOrExternalBerujuIdForDeleteParam);



                        foreach (var item in model.ToWhomDetailListVMList)
                        {
                            var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = 2 };
                            var InternalOrExternalIdParam = new SqlParameter { ParameterName = "@InternalOrExternalId", Value = PKID };
                            var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.EmployeeName == null ? string.Empty : item.EmployeeName };
                            var TotalAmountParam = new SqlParameter { ParameterName = "@TotalAmount", Value = item.AmountDetail.HasValue ? item.AmountDetail : 0 };
                            var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                            var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };
                            var VoucherNumberToWhomParam = new SqlParameter { ParameterName = "@VoucherNumberToWhom", Value = item.VoucherNumber == null ? string.Empty : item.VoucherNumber };
                            var VoucherDateToWhomParam = new SqlParameter { ParameterName = "@VoucherDateToWhom", Value = item.VoucherDate == null ? string.Empty : item.VoucherDate };


                            var MessageParamToWhom = new SqlParameter
                            {
                                ParameterName = "@MessageToWhom",
                                DbType = DbType.String,
                                Size = 50,
                                Direction = System.Data.ParameterDirection.Output
                            };

                            var resultToWhom = db.Database.ExecuteSqlCommand("exec InsertToWhomDetails @InternalOrExternal,@InternalOrExternalId,@PersonName,@TotalAmount,@PanNumber,@MobielNumber,@VoucherNumberToWhom,@VoucherDateToWhom,@MessageToWhom OUT", InternalOrExternalParam, InternalOrExternalIdParam, PersonNameParam, TotalAmountParam, PanNumberParam, MobielNumberParam,VoucherNumberToWhomParam,VoucherDateToWhomParam, MessageParamToWhom);



                        }
                    }


                }
                return returnModel;
            }

        }


        public ReturnMessageViewModel UpdateExternalBerujuByChecker(ExternalBeruju model, bool isChecker = false)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                var returnModel = new ReturnMessageViewModel();

                if (model.KoshTypeId != 1)
                    model.BudgetSubTitle = "001";

                // Prepare ToWhomDetails as DataTable
                DataTable toWhomTable = new DataTable();
                toWhomTable.Columns.Add("InternalOrExternal", typeof(int));
                toWhomTable.Columns.Add("InternalOrExternalId", typeof(int));
                toWhomTable.Columns.Add("PersonName", typeof(string));
                toWhomTable.Columns.Add("TotalAmount", typeof(decimal));
                toWhomTable.Columns.Add("PanNumber", typeof(string));
                toWhomTable.Columns.Add("MobielNumber", typeof(string));
                toWhomTable.Columns.Add("VoucherNumber", typeof(string));
                toWhomTable.Columns.Add("VoucherDate", typeof(DateTime));
                toWhomTable.Columns.Add("ToWhomStatus", typeof(int)); // required, not null

                if (model.ToWhomDetailListVMList != null && model.ToWhomDetailListVMList.Count > 0)
                {
                    foreach (var item in model.ToWhomDetailListVMList)
                    {
                        toWhomTable.Rows.Add(
                            2, // InternalOrExternal
                            model.ExternalBerujuId, // InternalOrExternalId
                            item.EmployeeName ?? string.Empty,
                            item.AmountDetail ?? 0,
                            item.PanNumber ?? string.Empty,
                            item.MobielNumber ?? string.Empty,
                            item.VoucherNumber ?? string.Empty,
                            item.VoucherDate ?? (object)DBNull.Value,
                            1 // default ToWhomStatus
                        );
                    }
                }

                // Prepare SP parameters
                var parameters = new List<SqlParameter>
        {
            new SqlParameter ("@UploadedFileUrl", model.UploadedFileUrl),
            new SqlParameter("@ExternalBerujuId", model.ExternalBerujuId),
            new SqlParameter("@FiscalYearId", model.FiscalYearId),
            new SqlParameter("@BudgetSubTitle", model.BudgetSubTitle),
            new SqlParameter("@ExpenseTItle", model.ExpenseTItle),
            new SqlParameter("@OfficeManagerName", model.OfficeManagerName ?? "Default"),
            new SqlParameter("@OfficeManagerPost", model.OfficeManagerPost ?? "Default"),
            new SqlParameter("@FromDate", model.FromDate),
            new SqlParameter("@ToDate", model.ToDate),
            new SqlParameter("@AccountantName", model.AccountantName ?? "Default"),
            new SqlParameter("@AccountantFromDate", model.AccountantFromDate),
            new SqlParameter("@AccountantToDate", model.AccountantToDate),
            new SqlParameter("@JVNUMBER", model.JVNUMBER),
            new SqlParameter("@VoucharDate", model.VoucharDate),
            new SqlParameter("@VoucharAmunt", model.VoucharAmunt),
            new SqlParameter("@BerujuDetails", model.BerujuDetails),
            new SqlParameter("@BerujuShorDesc", model.BerujuShorDesc ?? string.Empty),
            new SqlParameter("@BerujuTypeId", model.BerujuTypeId),
            new SqlParameter("@ToWhomID", model.ToWhomID),
            new SqlParameter("@ToWhomName", model.ToWhomName),
            new SqlParameter("@AuditorName", model.AuditorName ?? "Default"),
            new SqlParameter("@AuditorPost", model.AuditorPost ?? "Default"),
            new SqlParameter("@KoshTypeId", model.KoshTypeId),
            new SqlParameter("@BerujuNumber", model.BerujuNumber),
            new SqlParameter("@BerujuStatus", model.BerujuStatus),
            new SqlParameter("@InternalBerujuId", model.InternalBerujuId),
            new SqlParameter("@UpdatedBy", model.CreatedBy),
            new SqlParameter("@OfficeId", model.OfficeId),
            new SqlParameter("@OfficeManagerId", GetEmployeeOrEditorId(model.OfficeId, 1, model.VoucharDate)),
            new SqlParameter("@AccountantId", GetEmployeeOrEditorId(model.OfficeId, 2, model.VoucharDate)),
            new SqlParameter("@AuditorId", GetEmployeeOrEditorId(model.OfficeId, 3, model.VoucharDate)),
            new SqlParameter("@BerujuSubTitleId", model.BerujuSubTitleId ?? 0),
            new SqlParameter("@ChaluOrPujigat", model.ChaluOrPujigatId ?? 0),
            new SqlParameter("@ChaluOrPujigatTitleId", model.KoshTypeTitleListId ?? 0),
            new SqlParameter("@BerujuAmount", model.BerujuAmount ?? 0),
            new SqlParameter("@IsSaidantikBeruju", model.IsSaidantikBeruju),
            new SqlParameter("@ToWhomDetails", toWhomTable) { SqlDbType = SqlDbType.Structured, TypeName = "dbo.ToWhomDetailsType" },
            new SqlParameter("@IsChecker", isChecker ? 1 : 0),
            new SqlParameter("@Remarks", model.Remarks?? ""),
            new SqlParameter
            {
                ParameterName = "@Message",
                DbType = DbType.String,
                Size = 50,
                Direction = ParameterDirection.Output
            },
            new SqlParameter
            {
                ParameterName = "@PrimaryId",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Output
            }
        };

                // Execute SP
                db.Database.ExecuteSqlCommand(
                    "EXEC UpdateExternalBerujuByChecker @UploadedFileUrl, @ExternalBerujuId, @FiscalYearId, @BudgetSubTitle, @ExpenseTItle, @OfficeManagerName, @OfficeManagerPost, @FromDate, @ToDate, @AccountantName, @AccountantFromDate, @AccountantToDate, @JVNUMBER, @VoucharDate, @VoucharAmunt, @BerujuDetails, @BerujuShorDesc, @BerujuTypeId, @ToWhomID, @ToWhomName, @AuditorName, @AuditorPost, @KoshTypeId, @BerujuNumber, @BerujuStatus, @InternalBerujuId, @UpdatedBy, @OfficeId, @OfficeManagerId, @AccountantId, @AuditorId, @BerujuSubTitleId, @ChaluOrPujigat, @ChaluOrPujigatTitleId, @BerujuAmount, @IsSaidantikBeruju, @ToWhomDetails, @IsChecker,@Remarks,@Message OUT, @PrimaryId OUT",
                    parameters.ToArray()
                );

                returnModel.PrimaryId = (int)parameters.Last(p => p.ParameterName == "@PrimaryId").Value;
                returnModel.ReturnMessage = parameters.First(p => p.ParameterName == "@Message").Value.ToString();

                return returnModel;
            }
        }


        #region Samparikshad

        public List<SamparikhadListViewModel> GetExternalSamparikshadList(int OfficeId)
        {
            List<SamparikhadListViewModel> returnList = new List<SamparikhadListViewModel>();
            using (BerujuEntities db = new BerujuEntities())
            {


                returnList = db.Database.SqlQuery<SamparikhadListViewModel>("GetExternalSamparikshadList {0}", OfficeId).ToList();
                return returnList;
            }

        }

        public List<SamparikhadListViewModelForReport> GetExternalSamparikshadListForReport(int OfficeId, int FyId, int KoshTypeId)
        {
            List<SamparikhadListViewModelForReport> returnList = new List<SamparikhadListViewModelForReport>();
            using (BerujuEntities db = new BerujuEntities())
            {


                returnList = db.Database.SqlQuery<SamparikhadListViewModelForReport>("GetSamparikshadListForReport {0},{1},{2}", OfficeId, FyId, KoshTypeId).ToList();
                return returnList;
            }

        }


        public List<SamparikhadRequestListViewModelForReport> GetSamparikshadRequestListForReport(int OfficeId, int FyId, int KoshTypeId)
        {
            List<SamparikhadRequestListViewModelForReport> returnList = new List<SamparikhadRequestListViewModelForReport>();
            using (BerujuEntities db = new BerujuEntities())
            {


                returnList = db.Database.SqlQuery<SamparikhadRequestListViewModelForReport>("GetSamparikshadRequestListForReport {0},{1},{2}", OfficeId, FyId, KoshTypeId).ToList();
                return returnList;
            }

        }


        public List<SamparikhadRequestListViewModelForReport> GetSamparikshadRequestFromOffice(int OfficeId, int FyId, int KoshTypeId)
        {
            List<SamparikhadRequestListViewModelForReport> returnList = new List<SamparikhadRequestListViewModelForReport>();
            using (BerujuEntities db = new BerujuEntities())
            {


                returnList = db.Database.SqlQuery<SamparikhadRequestListViewModelForReport>("GetSamparikshadRequestFromOffice {0},{1},{2}", OfficeId, FyId, KoshTypeId).ToList();
                return returnList;
            }

        }


        public List<SamparikhadRequestListViewModelForReport> GetSamparikshadRequestOfOffice(int OfficeId, int FyId, int KoshTypeId)
        {
            List<SamparikhadRequestListViewModelForReport> returnList = new List<SamparikhadRequestListViewModelForReport>();
            using (BerujuEntities db = new BerujuEntities())
            {


                returnList = db.Database.SqlQuery<SamparikhadRequestListViewModelForReport>("GetSamparikshadRequestOfOffice {0},{1},{2}", OfficeId, FyId, KoshTypeId).ToList();
                return returnList;
            }

        }




        public ExternalSamparikshadViewModel GetExternalSamparikshadListByPrimaryId(int SamparikshadId, int OfficeId)
        {
            ExternalSamparikshadViewModel returnModel = new ExternalSamparikshadViewModel();
            using (BerujuEntities db = new BerujuEntities())
            {


                returnModel = db.Database.SqlQuery<ExternalSamparikshadViewModel>("GetExternalSamparikshadListByPrimaryId {0},{1}", OfficeId, SamparikshadId).FirstOrDefault();
                return returnModel;
            }

        }
        public InternalSamparikshadViewModel GetInternalSamparikshadListByPrimaryId(int SamparikshadId, int OfficeId)
        {
            InternalSamparikshadViewModel returnModel = new InternalSamparikshadViewModel();
            using (BerujuEntities db = new BerujuEntities())
            {


                returnModel = db.Database.SqlQuery<InternalSamparikshadViewModel>("IN_GetInternalSamparikshadListByPrimaryId {0},{1}", OfficeId, SamparikshadId).FirstOrDefault();
                return returnModel;
            }

        }


        public ReturnMessageViewModel InsertSamparikshadDetail(ExternalSamparikshadViewModel model)
        {

            using (BerujuEntities db = new BerujuEntities())
            {


                if (string.IsNullOrEmpty(model.UploadFileDetails))
                {
                    model.UploadFileDetails = string.Empty;
                }

                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = model.ExternalBerujuId };
                model.BerujuTypeId = 1;
                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var ReviesedVoucherAmountParam = new SqlParameter { ParameterName = "@ReviesedVoucherAmount", Value = model.ReviesedVoucherAmount };
                var RevisedDateParam = new SqlParameter { ParameterName = "@RevisedDate", Value = model.RevisedDate };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var UploadFileDetailsParam = new SqlParameter { ParameterName = "@UploadFileDetails", Value = model.UploadFileDetails };
                var LetterNumberParam = new SqlParameter { ParameterName = "@LetterNumber", Value = model.LetterNumber };
                var RevisedRemarksParam = new SqlParameter { ParameterName = "@RevisedRemarks", Value = model.RevisedRemarks };
                var MalepaOrKumariChowkIdParam = new SqlParameter { ParameterName = "@MalepaOrKumariChowkId", Value = model.MalepaOrKumariChowkId.HasValue ? model.MalepaOrKumariChowkId : 1 };
                var SamparikshadRequestMasterIdParam = new SqlParameter { ParameterName = "@RequestMasterId", Value = model.SamparikshadReqMasterId };
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


                var result = db.Database.ExecuteSqlCommand("exec InsertSamparikshadDetail @ExternalBerujuId,@BerujuTypeId,@ReviesedVoucherAmount,@RevisedDate,@OfficeId,@UploadFileDetails,@LetterNumber,@RevisedRemarks,@MalepaOrKumariChowkId,@RequestMasterId,@Message OUT,@PrimaryId OUT",
                        ExternalBerujuIdParam, BerujuTypeIdParam, ReviesedVoucherAmountParam, RevisedDateParam, OfficeIdParam, UploadFileDetailsParam, LetterNumberParam, RevisedRemarksParam, MalepaOrKumariChowkIdParam, SamparikshadRequestMasterIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                if (rms.PrimaryId > 0)
                {
                    //insert into to whom details
                    foreach (var item in model.SamparikshadTowhomDetailVMList)
                    {
                        var SMSamparikshadIdParam = new SqlParameter { ParameterName = "@SMSamparikshadId", Value = PKID };
                        var SMExternalBerujuIdParam = new SqlParameter { ParameterName = "@SMExternalBerujuId", Value = model.ExternalBerujuId };
                        var EBToWhomIdParam = new SqlParameter { ParameterName = "@EBToWhomId", Value = item.EBToWhomId };
                        var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.PersonName == null ? string.Empty : item.PersonName };
                        var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                        var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };
                        var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = item.BerujuAmount };
                        var RevisedAmountParam = new SqlParameter { ParameterName = "@RevisedAmount", Value = item.RevisedAmount.HasValue ? item.RevisedAmount : 0 };
                        var SamparikshadDateParam = new SqlParameter { ParameterName = "@SamparikshadDate", Value = model.RevisedDate };
                        var SMOfficeIdParam = new SqlParameter { ParameterName = "@SMOfficeId", Value = item.OfficeId };


                        var SMMessageParam = new SqlParameter
                        {
                            ParameterName = "@SMMessage",
                            DbType = DbType.String,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output
                        };

                        var SMresult = db.Database.ExecuteSqlCommand("exec InsertSamparikshadToWhomDetails @SMSamparikshadId,@SMExternalBerujuId,@EBToWhomId,@PersonName,@PanNumber,@MobielNumber,@BerujuAmount,@RevisedAmount,@SamparikshadDate,@SMOfficeId,@SMMessage OUT",
                       SMSamparikshadIdParam, SMExternalBerujuIdParam, EBToWhomIdParam, PersonNameParam, PanNumberParam, MobielNumberParam, BerujuAmountParam, RevisedAmountParam, SamparikshadDateParam, SMOfficeIdParam, SMMessageParam);

                    }


                    //update request master status....
                    var ReqMasterIdParam = new SqlParameter { ParameterName = "@ReqMasterId", Value = model.SamparikshadReqMasterId };
                    var UpdateRequestMasterParam = db.Database.ExecuteSqlCommand("exec SP_UpdateSamparikshadReqStatus @ReqMasterId",
                      ReqMasterIdParam);


                }

                return rms;

            }

        }

        public ReturnMessageViewModel IN_InsertInternalSamparikshadDetail(InternalSamparikshadViewModel model)
        {

            using (BerujuEntities db = new BerujuEntities())
            {


                if (string.IsNullOrEmpty(model.UploadFileDetails))
                {
                    model.UploadFileDetails = string.Empty;
                }

                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@InternalBerujuId", Value = model.InternalBerujuId };
                model.BerujuTypeId = 1;
                var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var ReviesedVoucherAmountParam = new SqlParameter { ParameterName = "@ReviesedVoucherAmount", Value = model.ReviesedVoucherAmount };
                var RevisedDateParam = new SqlParameter { ParameterName = "@RevisedDate", Value = model.RevisedDate };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var UploadFileDetailsParam = new SqlParameter { ParameterName = "@UploadFileDetails", Value = model.UploadFileDetails };
                var LetterNumberParam = new SqlParameter { ParameterName = "@LetterNumber", Value = model.LetterNumber };
                var RevisedRemarksParam = new SqlParameter { ParameterName = "@RevisedRemarks", Value = model.RevisedRemarks };
                var MalepaOrKumariChowkIdParam = new SqlParameter { ParameterName = "@MalepaOrKumariChowkId", Value = model.MalepaOrKumariChowkId.HasValue ? model.MalepaOrKumariChowkId : 1 };
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


                var result = db.Database.ExecuteSqlCommand("exec IN_InsertInternalSamparikshadDetail @InternalBerujuId,@BerujuTypeId,@ReviesedVoucherAmount,@RevisedDate,@OfficeId,@UploadFileDetails,@LetterNumber,@RevisedRemarks,@MalepaOrKumariChowkId,@Message OUT,@PrimaryId OUT",
                        ExternalBerujuIdParam, BerujuTypeIdParam, ReviesedVoucherAmountParam, RevisedDateParam, OfficeIdParam, UploadFileDetailsParam, LetterNumberParam, RevisedRemarksParam, MalepaOrKumariChowkIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                if (rms.PrimaryId > 0)
                {
                    //insert into to whom details
                    foreach (var item in model.SamparikshadTowhomDetailVMList)
                    {
                        var SMSamparikshadIdParam = new SqlParameter { ParameterName = "@InternalSMSamparikshadId", Value = PKID };
                        var SMExternalBerujuIdParam = new SqlParameter { ParameterName = "@SMInternalBerujuId", Value = model.InternalBerujuId };
                        var EBToWhomIdParam = new SqlParameter { ParameterName = "@IBToWhomId", Value = item.IBToWhomId };
                        var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.PersonName == null ? string.Empty : item.PersonName };
                        var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                        var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };
                        var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = item.BerujuAmount };
                        var RevisedAmountParam = new SqlParameter { ParameterName = "@RevisedAmount", Value = item.RevisedAmount.HasValue ? item.RevisedAmount : 0 };
                        var SamparikshadDateParam = new SqlParameter { ParameterName = "@SamparikshadDate", Value = model.RevisedDate };
                        var SMOfficeIdParam = new SqlParameter { ParameterName = "@SMOfficeId", Value = item.OfficeId };


                        var SMMessageParam = new SqlParameter
                        {
                            ParameterName = "@SMMessage",
                            DbType = DbType.String,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output
                        };

                        var SMresult = db.Database.ExecuteSqlCommand("exec IN_InsertInternalSamparikshadToWhomDetails @InternalSMSamparikshadId,@SMInternalBerujuId,@IBToWhomId,@PersonName,@PanNumber,@MobielNumber,@BerujuAmount,@RevisedAmount,@SamparikshadDate,@SMOfficeId,@SMMessage OUT",
                       SMSamparikshadIdParam, SMExternalBerujuIdParam, EBToWhomIdParam, PersonNameParam, PanNumberParam, MobielNumberParam, BerujuAmountParam, RevisedAmountParam, SamparikshadDateParam, SMOfficeIdParam, SMMessageParam);

                    }
                }

                return rms;

            }

        }

        public ReturnMessageViewModel UpdateSamparikshadDetail(ExternalSamparikshadViewModel model)
        {

            using (BerujuEntities db = new BerujuEntities())
            {


                if (string.IsNullOrEmpty(model.UploadFileDetails))
                {
                    model.UploadFileDetails = string.Empty;
                }

                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var SamparishadIdParam = new SqlParameter { ParameterName = "@SamparishadId", Value = model.SamparishadId };
                //var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = model.ExternalBerujuId };
                //var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var ReviesedVoucherAmountParam = new SqlParameter { ParameterName = "@ReviesedVoucherAmount", Value = model.ReviesedVoucherAmount };
                var RevisedDateParam = new SqlParameter { ParameterName = "@RevisedDate", Value = DateTime.Now };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var UploadFileDetailsParam = new SqlParameter { ParameterName = "@UploadFileDetails", Value = model.UploadFileDetails };
                var LetterNumberParam = new SqlParameter { ParameterName = "@LetterNumber", Value = model.LetterNumber };
                var RevisedRemarksParam = new SqlParameter { ParameterName = "@RevisedRemarks", Value = model.RevisedRemarks };
                var MalepaOrKumariChowkIdParam = new SqlParameter { ParameterName = "@MalepaOrKumariChowkId", Value = model.MalepaOrKumariChowkId.HasValue ? model.MalepaOrKumariChowkId : 1 };

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


                var result = db.Database.ExecuteSqlCommand("exec UpdateSamparikshadDetail @SamparishadId,@ReviesedVoucherAmount,@RevisedDate,@OfficeId,@UploadFileDetails,@LetterNumber,@RevisedRemarks,@MalepaOrKumariChowkId,@Message OUT,@PrimaryId OUT",
                        SamparishadIdParam, ReviesedVoucherAmountParam, RevisedDateParam, OfficeIdParam, UploadFileDetailsParam, LetterNumberParam, RevisedRemarksParam, MalepaOrKumariChowkIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                if (rms.PrimaryId > 0)
                {
                    var DelSamparikshadIdParam = new SqlParameter { ParameterName = "@DelSamparikshadId", Value = PKID };
                    var DelExternalBerujuIdParam = new SqlParameter { ParameterName = "@DelExternalBerujuId", Value = model.ExternalBerujuId };

                    //First delete
                    var DelMessageParam = new SqlParameter
                    {
                        ParameterName = "@DelMessage",
                        DbType = DbType.String,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };


                    var Delresult = db.Database.ExecuteSqlCommand("exec DeleteSamparikshadToWhomDetail @DelSamparikshadId,@DelExternalBerujuId,@DelMessage OUT", DelSamparikshadIdParam, DelExternalBerujuIdParam, DelMessageParam);
                    string DelMessage = DelMessageParam.SqlValue.ToString();

                    if (DelMessage == "Deleted")
                    {
                        //insert into to whom details
                        foreach (var item in model.SamparikshadTowhomDetailVMList)
                        {
                            var SMSamparikshadIdParam = new SqlParameter { ParameterName = "@SMSamparikshadId", Value = PKID };
                            var SMExternalBerujuIdParam = new SqlParameter { ParameterName = "@SMExternalBerujuId", Value = model.ExternalBerujuId };
                            var EBToWhomIdParam = new SqlParameter { ParameterName = "@EBToWhomId", Value = item.EBToWhomId };
                            var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.PersonName == null ? string.Empty : item.PersonName };
                            var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                            var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };
                            var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = item.BerujuAmount };
                            var RevisedAmountParam = new SqlParameter { ParameterName = "@RevisedAmount", Value = item.RevisedAmount.HasValue ? item.RevisedAmount : 0 };
                            var SamparikshadDateParam = new SqlParameter { ParameterName = "@SamparikshadDate", Value = model.RevisedDate };
                            var SMOfficeIdParam = new SqlParameter { ParameterName = "@SMOfficeId", Value = item.OfficeId };


                            var SMMessageParam = new SqlParameter
                            {
                                ParameterName = "@SMMessage",
                                DbType = DbType.String,
                                Size = 50,
                                Direction = System.Data.ParameterDirection.Output
                            };

                            var SMresult = db.Database.ExecuteSqlCommand("exec InsertSamparikshadToWhomDetails @SMSamparikshadId,@SMExternalBerujuId,@EBToWhomId,@PersonName,@PanNumber,@MobielNumber,@BerujuAmount,@RevisedAmount,@SamparikshadDate,@SMOfficeId,@SMMessage OUT",
                           SMSamparikshadIdParam, SMExternalBerujuIdParam, EBToWhomIdParam, PersonNameParam, PanNumberParam, MobielNumberParam, BerujuAmountParam, RevisedAmountParam, SamparikshadDateParam, SMOfficeIdParam, SMMessageParam);

                        }
                    }
                }



                return rms;






            }

        }

        public ReturnMessageViewModel UpdateInternalSamparikshadDetail(InternalSamparikshadViewModel model)
        {

            using (BerujuEntities db = new BerujuEntities())
            {


                if (string.IsNullOrEmpty(model.UploadFileDetails))
                {
                    model.UploadFileDetails = string.Empty;
                }

                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var SamparishadIdParam = new SqlParameter { ParameterName = "@InternalSamparishadId", Value = model.InternalSamparishadId };
                //var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = model.ExternalBerujuId };
                //var BerujuTypeIdParam = new SqlParameter { ParameterName = "@BerujuTypeId", Value = model.BerujuTypeId };
                var ReviesedVoucherAmountParam = new SqlParameter { ParameterName = "@ReviesedVoucherAmount", Value = model.ReviesedVoucherAmount };
                var RevisedDateParam = new SqlParameter { ParameterName = "@RevisedDate", Value = DateTime.Now };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var UploadFileDetailsParam = new SqlParameter { ParameterName = "@UploadFileDetails", Value = model.UploadFileDetails };
                var LetterNumberParam = new SqlParameter { ParameterName = "@LetterNumber", Value = model.LetterNumber };
                var RevisedRemarksParam = new SqlParameter { ParameterName = "@RevisedRemarks", Value = model.RevisedRemarks };
                var MalepaOrKumariChowkIdParam = new SqlParameter { ParameterName = "@MalepaOrKumariChowkId", Value = model.MalepaOrKumariChowkId.HasValue ? model.MalepaOrKumariChowkId : 1 };
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


                var result = db.Database.ExecuteSqlCommand("exec IN_UpdateInternalSamparikshadDetail @InternalSamparishadId,@ReviesedVoucherAmount,@RevisedDate,@OfficeId,@UploadFileDetails,@LetterNumber,@RevisedRemarks,@MalepaOrKumariChowkId,@Message OUT,@PrimaryId OUT",
                        SamparishadIdParam, ReviesedVoucherAmountParam, RevisedDateParam, OfficeIdParam, UploadFileDetailsParam, LetterNumberParam, RevisedRemarksParam, MalepaOrKumariChowkIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                if (rms.PrimaryId > 0)
                {
                    var DelSamparikshadIdParam = new SqlParameter { ParameterName = "@DelSamparikshadId", Value = PKID };
                    var DelExternalBerujuIdParam = new SqlParameter { ParameterName = "@DelInternalBerujuId", Value = model.InternalBerujuId };

                    //First delete
                    var DelMessageParam = new SqlParameter
                    {
                        ParameterName = "@DelMessage",
                        DbType = DbType.String,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };


                    var Delresult = db.Database.ExecuteSqlCommand("exec IN_DeleteSamparikshadToWhomDetail @DelSamparikshadId,@DelInternalBerujuId,@DelMessage OUT", DelSamparikshadIdParam, DelExternalBerujuIdParam, DelMessageParam);
                    string DelMessage = DelMessageParam.SqlValue.ToString();

                    if (DelMessage == "Deleted")
                    {
                        //insert into to whom details
                        foreach (var item in model.SamparikshadTowhomDetailVMList)
                        {
                            var SMSamparikshadIdParam = new SqlParameter { ParameterName = "@InternalSMSamparikshadId", Value = PKID };
                            var SMExternalBerujuIdParam = new SqlParameter { ParameterName = "@SMInternalBerujuId", Value = model.InternalBerujuId };
                            var EBToWhomIdParam = new SqlParameter { ParameterName = "@IBToWhomId", Value = item.IBToWhomId };
                            var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.PersonName == null ? string.Empty : item.PersonName };
                            var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                            var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };
                            var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = item.BerujuAmount };
                            var RevisedAmountParam = new SqlParameter { ParameterName = "@RevisedAmount", Value = item.RevisedAmount.HasValue ? item.RevisedAmount : 0 };
                            var SamparikshadDateParam = new SqlParameter { ParameterName = "@SamparikshadDate", Value = model.RevisedDate };
                            var SMOfficeIdParam = new SqlParameter { ParameterName = "@SMOfficeId", Value = item.OfficeId };


                            var SMMessageParam = new SqlParameter
                            {
                                ParameterName = "@SMMessage",
                                DbType = DbType.String,
                                Size = 50,
                                Direction = System.Data.ParameterDirection.Output
                            };

                            var SMresult = db.Database.ExecuteSqlCommand("exec IN_InsertInternalSamparikshadToWhomDetails @InternalSMSamparikshadId,@SMInternalBerujuId,@IBToWhomId,@PersonName,@PanNumber,@MobielNumber,@BerujuAmount,@RevisedAmount,@SamparikshadDate,@SMOfficeId,@SMMessage OUT",
                           SMSamparikshadIdParam, SMExternalBerujuIdParam, EBToWhomIdParam, PersonNameParam, PanNumberParam, MobielNumberParam, BerujuAmountParam, RevisedAmountParam, SamparikshadDateParam, SMOfficeIdParam, SMMessageParam);

                        }
                    }
                }
                

                return rms;


            }

        }



        public ReturnMessageViewModel DeleteSamparikshadDetail(int SamparkishadId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var SamparikshadIdParam = new SqlParameter { ParameterName = "@SamparikshadId", Value = SamparkishadId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@DelMessage",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec DeleteSamparikshadDetail @SamparikshadId,@DelMessage OUT", SamparikshadIdParam, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = SamparkishadId;
                return returnModel;
            }

        }


        public ReturnMessageViewModel DeleteInternalSamparikshadDetail(int SamparkishadId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var SamparikshadIdParam = new SqlParameter { ParameterName = "@SamparikshadId", Value = SamparkishadId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@DelMessage",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec IN_DeleteSamparikshadDetail @SamparkishadId,@DelMessage OUT", SamparikshadIdParam, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = SamparkishadId;
                return returnModel;
            }

        }


        #endregion

        #region Saidaintik Beruju

        public SaidantikBeruju spGetSaindantikberujupagination(int PageNumer, int pageSized, int InternalOrExternal)
        {
            using (BerujuEntities db = new BerujuEntities())
            {

                SaidantikBeruju model = new SaidantikBeruju();
                List<SaidantikBeruju> ReturnList = new List<SaidantikBeruju>();
                var PageNumberParam = new SqlParameter { ParameterName = "@PageNumber", Value = PageNumer };
                var PageSizeParam = new SqlParameter { ParameterName = "@PageSize", Value = pageSized };
                var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = InternalOrExternal };

                var TotalCountParam = new SqlParameter
                {
                    ParameterName = "@TotalRecordCount",
                    DbType = DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };

                ReturnList = db.Database.SqlQuery<SaidantikBeruju>("spGetSaindantikberujupagination @PageNumber,@PageSize,@InternalOrExternal,@TotalRecordCount OUT",
                   new object[] { PageNumberParam, PageSizeParam, InternalOrExternalParam, TotalCountParam }).ToList();
                int totalCountRcd = (int)TotalCountParam.Value;

                model.SaidantikBerujuList = ReturnList;
                model.PageCount = totalCountRcd;
                return model;
            }
        }

        public List<SaidantikBeruju> ListSaidantikBeruju(int OfficeId, int InternalOrExternal)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<SaidantikBeruju> ReturnList = new List<SaidantikBeruju>();
                ReturnList = db.Database.SqlQuery<SaidantikBeruju>("ListSaidantikBeruju {0},{1}", OfficeId, InternalOrExternal).ToList();
                return ReturnList;
            }
        }

        public List<SaidantikBeruju> ListSaidantikBerujuForAdmin(int OfficeId, int InternalOrExternal, int FiscalYearId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<SaidantikBeruju> ReturnList = new List<SaidantikBeruju>();
                ReturnList = db.Database.SqlQuery<SaidantikBeruju>("ListSaidantikBerujuForAdmin {0},{1},{2}", OfficeId, InternalOrExternal, FiscalYearId).ToList();
                return ReturnList;
            }
        }

        public List<SaidantikBeruju> ListSaidantikBerujuTopFive(int OfficeId, int InternalOrExternal)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<SaidantikBeruju> ReturnList = new List<SaidantikBeruju>();
                ReturnList = db.Database.SqlQuery<SaidantikBeruju>("ListSaidantikBerujuTopFive {0},{1}", OfficeId, InternalOrExternal).ToList();
                return ReturnList;
            }
        }


        public SaidantikBeruju GetSaidantikBerujuByPrimaryId(int OfficeId, int PrimaryId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                SaidantikBeruju ReturnModel = new SaidantikBeruju();
                ReturnModel = db.Database.SqlQuery<SaidantikBeruju>("GetSaidantikBerujuByPrimaryId {0},{1}", OfficeId, PrimaryId).FirstOrDefault();
                if (ReturnModel == null)
                {
                    return ReturnModel = new SaidantikBeruju();
                }
                return ReturnModel;
            }
        }

        public ReturnMessageViewModel InsertSaidantikBeruju(SaidantikBeruju model)
        {

            using (BerujuEntities db = new BerujuEntities())
            {


                //if (string.IsNullOrEmpty(model.UploadFileDetails))
                //{
                //    model.UploadFileDetails = string.Empty;
                //}
                model.BerujuStatus = false;
                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
                var BerujuDafaNumberParam = new SqlParameter { ParameterName = "@BerujuDafaNumber", Value = model.BerujuDafaNumber };
                var BerujuShortDescParam = new SqlParameter { ParameterName = "@BerujuShortDesc", Value = model.BerujuShortDesc };
                var BerujuLongDescParam = new SqlParameter { ParameterName = "@BerujuLongDesc", Value = model.BerujuLongDesc };
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = model.BerujuStatus };
                var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = model.InternalOrExternal };

                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var BudgetSubTitleIdParam = new SqlParameter { ParameterName = "@BudgetSubTitleId", Value = model.BudgetSubTitleId.HasValue ? model.BudgetSubTitleId : 0 };
                var SaidantikDocparam = new SqlParameter { ParameterName = "@SaidantikDoc", Value = model.SaidantikDoc == null ? string.Empty : model.SaidantikDoc };
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


                var result = db.Database.ExecuteSqlCommand("exec InsertSaidantikBeruju @FiscalYearId,@BerujuDafaNumber,@BerujuShortDesc,@BerujuLongDesc,@BerujuStatus,@InternalOrExternal,@OfficeId,@BudgetSubTitleId,@SaidantikDoc,@Message OUT,@PrimaryId OUT",
                        FiscalYearIdParam, BerujuDafaNumberParam, BerujuShortDescParam, BerujuLongDescParam, BerujuStatusParam, InternalOrExternalParam, OfficeIdParam, BudgetSubTitleIdParam, SaidantikDocparam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                return rms;

            }

        }


        public ReturnMessageViewModel UpdpateSaidantikBeruju(SaidantikBeruju model)
        {

            using (BerujuEntities db = new BerujuEntities())
            {


                //if (string.IsNullOrEmpty(model.UploadFileDetails))
                //{
                //    model.UploadFileDetails = string.Empty;
                //}
                model.BerujuStatus = false;
                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var SaidantikBerujuIdParam = new SqlParameter { ParameterName = "@SaidantikBerujuId", Value = model.SaidantikBerujuId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
                var BerujuDafaNumberParam = new SqlParameter { ParameterName = "@BerujuDafaNumber", Value = model.BerujuDafaNumber };
                var BerujuShortDescParam = new SqlParameter { ParameterName = "@BerujuShortDesc", Value = model.BerujuShortDesc };
                var BerujuLongDescParam = new SqlParameter { ParameterName = "@BerujuLongDesc", Value = model.BerujuLongDesc };
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = model.BerujuStatus };
                var BudgetSubTitleIdParam = new SqlParameter { ParameterName = "@BudgetSubTitleId", Value = model.BudgetSubTitleId.HasValue ? model.BudgetSubTitleId : 0 };
                var SaidantikDocparam = new SqlParameter { ParameterName = "@SaidantikDoc", Value = model.SaidantikDoc == null ? string.Empty : model.SaidantikDoc };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec UpdateSaidantikBeruju @SaidantikBerujuId,@FiscalYearId,@BerujuDafaNumber,@BerujuShortDesc,@BerujuLongDesc,@BerujuStatus,@BudgetSubTitleId,@SaidantikDoc,@Message OUT",
                        SaidantikBerujuIdParam, FiscalYearIdParam, BerujuDafaNumberParam, BerujuShortDescParam, BerujuLongDescParam, BerujuStatusParam, BudgetSubTitleIdParam, SaidantikDocparam, MessageParam);
                int PKID = model.SaidantikBerujuId;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                return rms;

            }

        }

        public ReturnMessageViewModel UpdpateSaidantikBerujuFromChecker(SaidantikBeruju model)
        {

            using (BerujuEntities db = new BerujuEntities())
            {


                //if (string.IsNullOrEmpty(model.UploadFileDetails))
                //{
                //    model.UploadFileDetails = string.Empty;
                //}
                model.BerujuStatus = true;
                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var SaidantikBerujuIdParam = new SqlParameter { ParameterName = "@SaidantikBerujuId", Value = model.SaidantikBerujuId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
                var BerujuDafaNumberParam = new SqlParameter { ParameterName = "@BerujuDafaNumber", Value = model.BerujuDafaNumber };
                var BerujuShortDescParam = new SqlParameter { ParameterName = "@BerujuShortDesc", Value = model.BerujuShortDesc };
                var BerujuLongDescParam = new SqlParameter { ParameterName = "@BerujuLongDesc", Value = model.BerujuLongDesc };
                var BerujuStatusParam = new SqlParameter { ParameterName = "@BerujuStatus", Value = model.BerujuStatus };
                var BudgetSubTitleIdParam = new SqlParameter { ParameterName = "@BudgetSubTitleId", Value = model.BudgetSubTitleId.HasValue ? model.BudgetSubTitleId : 0 };
                var SaidantikDocparam = new SqlParameter { ParameterName = "@SaidantikDoc", Value = model.SaidantikDoc == null ? string.Empty : model.SaidantikDoc };
                var UpdatedByparam = new SqlParameter { ParameterName = "@UpdatedBy", Value = model.CreatedBy };
                var IsCheckerparam = new SqlParameter { ParameterName = "@IsChecker", Value = true };
                var Remarks = new SqlParameter { ParameterName = "@Remarks", Value = model.Remarks ?? "" };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec UpdateSaidantikBerujuFromChecker @SaidantikBerujuId,@FiscalYearId,@BerujuDafaNumber,@BerujuShortDesc,@BerujuLongDesc,@BerujuStatus,@BudgetSubTitleId,@SaidantikDoc, @UpdatedBy, @IsChecker,@Remarks,@Message OUT",
                        SaidantikBerujuIdParam, FiscalYearIdParam, BerujuDafaNumberParam, BerujuShortDescParam, BerujuLongDescParam, BerujuStatusParam, BudgetSubTitleIdParam, SaidantikDocparam,UpdatedByparam,IsCheckerparam,Remarks, MessageParam);
                int PKID = model.SaidantikBerujuId;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                return rms;

            }

        }

        public ReturnMessageViewModel DeleteSaidantikBeruju(int SaidantikBerujuId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var SaidantikBerujuIdParam = new SqlParameter { ParameterName = "@SaidantikBerujuId", Value = SaidantikBerujuId };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec DeleteSaidantikBeruju @SaidantikBerujuId,@Message OUT", SaidantikBerujuIdParam, MessageParam);
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = 0;
                return returnModel;
            }

        }

        public ReturnMessageViewModel TransferBeruju(BerujuCheckerReportFilter model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {


               
                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = model.ExternalBeruju.ExternalBerujuId };
                var TransferOfficeIdParam = new SqlParameter { ParameterName = "@TransferOfficeId", Value = model.TransferOfficeId };
                var TransferStatus = new SqlParameter { ParameterName = "@TransferStatus", Value = model.Status };
                var CreatedByParam = new SqlParameter { ParameterName = "@CreatedBy", Value = model.CreatedBy };

                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };


                var result = db.Database.ExecuteSqlCommand("exec TransferBeruju @ExternalBerujuId,@TransferOfficeId,@TransferStatus,@CreatedBy,@Message OUT",
                      ExternalBerujuIdParam, TransferOfficeIdParam,TransferStatus,CreatedByParam, MessageParam);
                int PKID = model.ExternalBeruju.ExternalBerujuId;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                return rms;

            }
        }


        #endregion
        public ReturnMessageViewModel InsertSamparikshadToWhomDetails(ExternalBeruju model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                foreach (var item in model.ToWhomDetailListVMList)
                {
                    var SamparikshadIdParam = new SqlParameter { ParameterName = "@SamparikshadId", Value = model.ObjExternalSamparikshadViewModel.SamparishadId };
                    var @ExternalBerujuIdParam = new SqlParameter { ParameterName = "@@ExternalBerujuId", Value = model.ExternalBerujuId };

                }

                return returnModel;
            }
        }



        public decimal GetSamparikshadRemainingAmount(int ExternalBerujuId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                decimal RemainingAmount = 0;
                RemainingAmount = db.Database.SqlQuery<decimal>("GetSamparikshadRemainingAmount {0}", ExternalBerujuId).FirstOrDefault();
                return RemainingAmount;

            }
        }



        public decimal IN_GetInternalSamparikshadRemainingAmount(int InternalBerujuId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                decimal RemainingAmount = 0;
                RemainingAmount = db.Database.SqlQuery<decimal>("IN_GetInternalSamparikshadRemainingAmount {0}", InternalBerujuId).FirstOrDefault();
                return RemainingAmount;

            }
        }

        public decimal GetSamparikshadRemainingAmountForRequest(int ExternalBerujuId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                decimal RemainingAmount = 0;
                RemainingAmount = db.Database.SqlQuery<decimal>("GetSamparikshadRemainingAmountForRequest {0}", ExternalBerujuId).FirstOrDefault();
                return RemainingAmount;

            }
        }

        public List<InternalSamparikshadTowhomDetailVM> ListInternalSamparikshadTowhomDetails(int InternalBerujuId, int OfficeId, int InternalSamparikshadId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<InternalSamparikshadTowhomDetailVM> ReturnList = new List<InternalSamparikshadTowhomDetailVM>();

                ReturnList = db.Database.SqlQuery<InternalSamparikshadTowhomDetailVM>("IN_GetInternalSamparikshadToWhomDetailsByBerujuId {0},{1},{2}", InternalBerujuId, OfficeId, InternalSamparikshadId).ToList();
                return ReturnList;
            }
        }

        public List<SamparikshadTowhomDetailVM> ListSamparikshadTowhomDetails(int ExternalBerujuId, int OfficeId, int SamparikshadId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<SamparikshadTowhomDetailVM> ReturnList = new List<SamparikshadTowhomDetailVM>();

                ReturnList = db.Database.SqlQuery<SamparikshadTowhomDetailVM>("GetSamparikshadToWhomDetailsByBerujuId {0},{1},{2}", ExternalBerujuId, OfficeId, SamparikshadId).ToList();
                return ReturnList;
            }
        }


        public List<SamparikshadTowhomDetailVM> ListSamparikshadTowhomDetailsForRequest(int ExternalBerujuId, int OfficeId, int SamparikshadReqId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<SamparikshadTowhomDetailVM> ReturnList = new List<SamparikshadTowhomDetailVM>();

                ReturnList = db.Database.SqlQuery<SamparikshadTowhomDetailVM>("GetSamparikshadToWhomDetailsByBerujuIdForRequest {0},{1},{2}", ExternalBerujuId, OfficeId, SamparikshadReqId).ToList();
                return ReturnList;
            }
        }

        public List<InternalSamparikshadTowhomDetailVM> IN_GetSamparikshadToWhomDetailsByBerujuIdForRequest(int InternalBerujuId, int OfficeId, int SamparikshadReqId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<InternalSamparikshadTowhomDetailVM> ReturnList = new List<InternalSamparikshadTowhomDetailVM>();

                ReturnList = db.Database.SqlQuery<InternalSamparikshadTowhomDetailVM>("IN_GetSamparikshadToWhomDetailsByBerujuIdForRequest {0},{1},{2}", InternalBerujuId, OfficeId, SamparikshadReqId).ToList();
                return ReturnList;
            }
        }




        public List<SamparikshadRequestTowhomDetailVM> ListSamparikshadRequestTowhomDetails(int ExternalBerujuId, int OfficeId, int SamparikshadId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<SamparikshadRequestTowhomDetailVM> ReturnList = new List<SamparikshadRequestTowhomDetailVM>();

                ReturnList = db.Database.SqlQuery<SamparikshadRequestTowhomDetailVM>("GetSamparikshadToWhomDetailsByBerujuId {0},{1},{2}", ExternalBerujuId, OfficeId, SamparikshadId).ToList();
                return ReturnList;
            }
        }

        public string GetFiscalyearTitleFromFiscalyearId(int FyId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                string fiscalyeraTitle = db.Database.SqlQuery<string>(@"select FiscalYearTitleEng From FiscalYearRecord where FiscalYearId='" + FyId + "'").FirstOrDefault();
                return fiscalyeraTitle.ToString();

            }

        }

        public ReturnMessageViewModel SendForSamparikchan(SamparikshadReqMasterViewModel model)
        {

        
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var ReqMasterIdParam = new SqlParameter { ParameterName = "@MasterId", Value = model.SamparikshadReqMasterId };
                var ReqOfficeIdParam = new SqlParameter { ParameterName = "@RequestedFromOfficeId", Value = model.OfficeId };
                var ReqToWhomofficeIdParam = new SqlParameter { ParameterName = "@RequestedToOfficeId", Value = model.ToWhomofficeId };
                var ReqRemarksForRequestParam = new SqlParameter { ParameterName = "@Remarks", Value = model.RemarksForRequest == null ? string.Empty : model.RemarksForRequest };
                var ReqMinistryParam = new SqlParameter { ParameterName = "@RequestToMinistry", Value = model.RequestToId };
                var ReqOfficeMessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Direction = ParameterDirection.Output
                };





                try
                {

                    var insertSamparikshanRequestOffice = db.Database.ExecuteSqlCommand("exec InsertSamparikshadReqOffice @MasterId,@RequestedFromOfficeId,@RequestedToOfficeId,@Remarks,@RequestToMinistry,@Message OUT",
                                                        ReqMasterIdParam, ReqOfficeIdParam, ReqToWhomofficeIdParam, ReqRemarksForRequestParam, ReqMinistryParam, ReqOfficeMessageParam);
                    rms.ReturnMessage = ReqOfficeMessageParam.Value?.ToString();
                }
                catch (Exception e)
                {
                    string error = e.ToString();

                }

                return rms;

            }
        }




        public ReturnMessageViewModel InsertSamparikshadReqDetail(SamparikshadReqMasterViewModel model)
        {

            using (BerujuEntities db = new BerujuEntities())
            {
                model.OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
                var DelExternalBerujuIdParam = new SqlParameter { ParameterName = "@DelExternalBerujuId", Value = model.ExternalBerujuId };
                var DelOfficeIdParam = new SqlParameter { ParameterName = "@DelOfficeId", Value = model.OfficeId };
                var DelMessageParam = new SqlParameter
                {
                    ParameterName = "@DelMessage",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                //var Deleteresult = db.Database.ExecuteSqlCommand("exec SPDeleteSamparikshadReqMasterDetail @DelExternalBerujuId,@DelOfficeId,@DelMessage OUT", DelExternalBerujuIdParam, DelOfficeIdParam, DelMessageParam);

                //model.RequestedDateEng = DateTime.Now;
                //model.RequestedDateNep = Utilities.GetNepaliDateFromEng(model.RequestedDateEng);


                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var ToWhomMinistryNameParam = new SqlParameter { ParameterName = "@ToWhomMinistryName", Value = model.ToWhomMinistryName == null ? string.Empty : model.ToWhomMinistryName };
                var ToWhomDeptNameParam = new SqlParameter { ParameterName = "@ToWhomDeptName", Value = model.ToWhomDeptName == null ? string.Empty : model.ToWhomDeptName };
                var ToWhomOfficeNameParam = new SqlParameter { ParameterName = "@ToWhomOfficeName", Value = model.ToWhomOfficeName == null ? string.Empty : model.ToWhomOfficeName };
                var OfficeAddressParam = new SqlParameter { ParameterName = "@OfficeAddress", Value = model.OfficeAddress == null ? string.Empty : model.OfficeAddress };
                var RequestedDateEngParam = new SqlParameter { ParameterName = "@RequestedDateEng", Value = model.RequestedDateEng };
                var RequestedDateNepParam = new SqlParameter { ParameterName = "@RequestedDateNep", Value = model.RequestedDateNep };
                string letternumber = GetFiscalyearTitleFromFiscalyearId(model.FYID);
                var LetterNumberParam = new SqlParameter { ParameterName = "@LetterNumber", Value = letternumber == null ? string.Empty : letternumber };
                var FYIDParam = new SqlParameter { ParameterName = "@FYID", Value = model.FYID };
                var TotalAmountParam = new SqlParameter { ParameterName = "@TotalAmount", Value = model.TotalAmount };
                var RemarksParam = new SqlParameter { ParameterName = "@Remarks", Value = model.Remarks == null ? string.Empty : model.Remarks };
                var ResponsiblePersonNameParam = new SqlParameter { ParameterName = "@ResponsiblePersonName", Value = model.ResponsiblePersonName == null ? string.Empty : model.ResponsiblePersonName };
                var PostParam = new SqlParameter { ParameterName = "@Post", Value = model.Post == null ? string.Empty : model.Post };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var UploadedDocParam = new SqlParameter { ParameterName = "@UploadedDoc", Value = model.UploadedDoc == null ? string.Empty : model.UploadedDoc };
                var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = model.ExternalBerujuId };
                var ToWhomofficeIdParam = new SqlParameter { ParameterName = "@ToWhomofficeId", Value = model.ToWhomofficeId };
                var RemarksForRequestParam = new SqlParameter { ParameterName = "@RemarksForRequest", Value = model.RemarksForRequest == null ? string.Empty : model.RemarksForRequest };
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


                var result = db.Database.ExecuteSqlCommand("exec InsertSamparikshadReqMaster @ToWhomMinistryName,@ToWhomDeptName,@ToWhomOfficeName,@OfficeAddress,@RequestedDateEng,@RequestedDateNep,@LetterNumber,@FYID,@TotalAmount,@Remarks,@ResponsiblePersonName,@Post,@OfficeId,@UploadedDoc,@ExternalBerujuId,@ToWhomofficeId,@RemarksForRequest,@Message OUT,@PrimaryId OUT",
                        ToWhomMinistryNameParam, ToWhomDeptNameParam, ToWhomOfficeNameParam, OfficeAddressParam, RequestedDateEngParam, RequestedDateNepParam, LetterNumberParam, FYIDParam, TotalAmountParam, RemarksParam, ResponsiblePersonNameParam, PostParam, OfficeIdParam, UploadedDocParam, ExternalBerujuIdParam, ToWhomofficeIdParam, RemarksForRequestParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                if (PKID > 0)
                {
                    model.ObjSamparikshadReqDetailViewModel = new SamparikshadReqDetailViewModel();
                    var MasterIdParam = new SqlParameter { ParameterName = "@MasterId", Value = PKID };
                    var InternalOrExteranlBerujuIdParam = new SqlParameter { ParameterName = "@InternalOrExteranlBerujuId", Value = model.ExternalBerujuId };
                    var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = 2 };
                    var BerujuDafaNumberParam = new SqlParameter { ParameterName = "@BerujuDafaNumber", Value = model.BerujuDafaNumber == null ? string.Empty : model.BerujuDafaNumber };
                    var BerujuShortDesParam = new SqlParameter { ParameterName = "@BerujuShortDes", Value = model.BerujuShortDescription == null ? string.Empty : model.BerujuShortDescription };
                    var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = model.TotalAmount };

                    var MessageParamDetail = new SqlParameter
                    {
                        ParameterName = "@MessageDetail",
                        DbType = DbType.String,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };

                    try
                    {
                        var insertRequestDetails = db.Database.ExecuteSqlCommand("exec InsertSamparikshadReqDetail @MasterId,@InternalOrExteranlBerujuId,@InternalOrExternal,@BerujuDafaNumber,@BerujuShortDes,@BerujuAmount,@MessageDetail OUT",
                        MasterIdParam, InternalOrExteranlBerujuIdParam, InternalOrExternalParam, BerujuDafaNumberParam, BerujuShortDesParam, BerujuAmountParam, MessageParamDetail);


                    }
                    catch (Exception e)
                    {
                        string error = e.ToString();

                    }

                    var ReqMasterIdParam = new SqlParameter { ParameterName = "@MasterId", Value = PKID };
                    var ReqOfficeIdParam = new SqlParameter { ParameterName = "@RequestedFromOfficeId", Value = model.OfficeId };
                    var ReqToWhomofficeIdParam = new SqlParameter { ParameterName = "@RequestedToOfficeId", Value = model.ToWhomofficeId };
                    var ReqRemarksForRequestParam = new SqlParameter { ParameterName = "@Remarks", Value = model.RemarksForRequest == null ? string.Empty : model.RemarksForRequest };
                    var ReqMinistryParam = new SqlParameter { ParameterName = "@RequestToMinistry", Value = 0 };
                    var ReqOfficeMessageParam = new SqlParameter
                    {
                        ParameterName = "@Message",
                        DbType = DbType.String,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };


                    try
                    {

                        var insertSamparikshanRequestOffice = db.Database.ExecuteSqlCommand("exec InsertSamparikshadReqOffice @MasterId,@RequestedFromOfficeId,@RequestedToOfficeId,@Remarks,@RequestToMinistry,@Message OUT",
                                                               ReqMasterIdParam, ReqOfficeIdParam, ReqToWhomofficeIdParam, ReqRemarksForRequestParam,ReqMinistryParam, ReqOfficeMessageParam);

                    }
                    catch (Exception e)
                    {
                        string error = e.ToString();

                    }





                    //delete data from samparikshad req to whom details...
                    //var DeleteresultToWhomDetails = db.Database.ExecuteSqlCommand("exec SPDeleteSamparikshadReqMasterDetail @DelExternalBerujuId,@DelOfficeId,@DelMessage OUT", DelExternalBerujuIdParam, DelOfficeIdParam, DelMessageParam);


                    foreach (var item in model.SamparikshadTowhomDetailVMList)
                    {
                        string FileNameVal = item.SupportingDocFiles == null ? string.Empty : item.SupportingDocFiles.FileName;
                        if (!string.IsNullOrEmpty(FileNameVal))
                        {
                            string concateletter = "S-R-" + item.SMTowhomDetailId + item.ExternalBerujuId;
                            item.UploadedFileUrl = concateletter + "_" + item.SupportingDocFiles.FileName;
                        }
                        else
                        {
                            item.UploadedFileUrl = string.Empty;
                        }

                        var SMSamparikshadIdParam = new SqlParameter { ParameterName = "@SMSamparikshadId", Value = PKID };
                        var SMExternalBerujuIdParam = new SqlParameter { ParameterName = "@SMExternalBerujuId", Value = model.ExternalBerujuId };
                        var EBToWhomIdParam = new SqlParameter { ParameterName = "@EBToWhomId", Value = item.EBToWhomId };
                        var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.PersonName == null ? string.Empty : item.PersonName };
                        var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                        var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };
                        var BerujuAmountReqestParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = item.BerujuAmount };
                        var RevisedAmountParam = new SqlParameter { ParameterName = "@RevisedAmount", Value = item.RevisedAmount.HasValue ? item.RevisedAmount : 0 };
                        var SamparikshadDateParam = new SqlParameter { ParameterName = "@SamparikshadDate", Value = model.RequestedDateEng };
                        var SMOfficeIdParam = new SqlParameter { ParameterName = "@SMOfficeId", Value = item.OfficeId };
                        var UploadedFileUrlParam = new SqlParameter { ParameterName = "@UploadedFileUrl", Value = item.UploadedFileUrl };

                        var SMMessageParam = new SqlParameter
                        {
                            ParameterName = "@SMMessage",
                            DbType = DbType.String,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output
                        };

                        var SMresult = db.Database.ExecuteSqlCommand("exec InsertSamparikshadReqToWhomDetails @SMSamparikshadId,@SMExternalBerujuId,@EBToWhomId,@PersonName,@PanNumber,@MobielNumber,@BerujuAmount,@RevisedAmount,@SamparikshadDate,@SMOfficeId,@UploadedFileUrl,@SMMessage OUT",
                       SMSamparikshadIdParam, SMExternalBerujuIdParam, EBToWhomIdParam, PersonNameParam, PanNumberParam, MobielNumberParam, BerujuAmountReqestParam, RevisedAmountParam, SamparikshadDateParam, SMOfficeIdParam, UploadedFileUrlParam, SMMessageParam);

                    }




                    //if (model.SamparikshadReqDetailViewModelList.Count > 0)
                    //{


                    //    foreach (var item in model.SamparikshadReqDetailViewModelList)
                    //    {
                    //        var MasterIdParam = new SqlParameter { ParameterName = "@MasterId", Value = PKID };
                    //        var InternalOrExteranlBerujuIdParam = new SqlParameter { ParameterName = "@InternalOrExteranlBerujuId", Value = item.InternalOrExteranlBerujuId };
                    //        var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = item.InternalOrExternal };
                    //        var BerujuDafaNumberParam = new SqlParameter { ParameterName = "@BerujuDafaNumber", Value = item.BerujuDafaNumber};
                    //        var BerujuShortDesParam = new SqlParameter { ParameterName = "@BerujuShortDes", Value = item.BerujuShortDes == null ? string.Empty : item.BerujuShortDes };
                    //        var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = item.BerujuAmount };


                    //        var MessageParamDetail = new SqlParameter
                    //        {
                    //            ParameterName = "@MessageDetail",
                    //            DbType = DbType.String,
                    //            Size = 50,
                    //            Direction = System.Data.ParameterDirection.Output
                    //        };

                    //        var resultToWhom = db.Database.ExecuteSqlCommand("exec InsertSamparikshadReqDetail @MasterId,@InternalOrExteranlBerujuId,@InternalOrExternal,@BerujuDafaNumber,@BerujuShortDes,@BerujuAmount,@MessageDetail OUT", 
                    //            MasterIdParam, InternalOrExteranlBerujuIdParam, InternalOrExternalParam, BerujuDafaNumberParam, BerujuShortDesParam, BerujuAmountParam, MessageParamDetail);



                    //    }
                    //}
                }



                return rms;

            }

        }


        public ReturnMessageViewModel IN_InsertInternalSamparikshadReqDetail(InternalSamparikshadReqMasterViewModel model)
        {

            using (BerujuEntities db = new BerujuEntities())
            {
                model.OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
                var DelExternalBerujuIdParam = new SqlParameter { ParameterName = "@DelExternalBerujuId", Value = model.InternalBerujuId };
                var DelOfficeIdParam = new SqlParameter { ParameterName = "@DelOfficeId", Value = model.OfficeId };
                var DelMessageParam = new SqlParameter
                {
                    ParameterName = "@DelMessage",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                //var Deleteresult = db.Database.ExecuteSqlCommand("exec SPDeleteSamparikshadReqMasterDetail @DelExternalBerujuId,@DelOfficeId,@DelMessage OUT", DelExternalBerujuIdParam, DelOfficeIdParam, DelMessageParam);

                //model.RequestedDateEng = DateTime.Now;
                //model.RequestedDateNep = Utilities.GetNepaliDateFromEng(model.RequestedDateEng);


                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var ToWhomMinistryNameParam = new SqlParameter { ParameterName = "@ToWhomMinistryName", Value = model.ToWhomMinistryName == null ? string.Empty : model.ToWhomMinistryName };
                var ToWhomDeptNameParam = new SqlParameter { ParameterName = "@ToWhomDeptName", Value = model.ToWhomDeptName == null ? string.Empty : model.ToWhomDeptName };
                var ToWhomOfficeNameParam = new SqlParameter { ParameterName = "@ToWhomOfficeName", Value = model.ToWhomOfficeName == null ? string.Empty : model.ToWhomOfficeName };
                var OfficeAddressParam = new SqlParameter { ParameterName = "@OfficeAddress", Value = model.OfficeAddress == null ? string.Empty : model.OfficeAddress };
                var RequestedDateEngParam = new SqlParameter { ParameterName = "@RequestedDateEng", Value = model.RequestedDateEng };
                var RequestedDateNepParam = new SqlParameter { ParameterName = "@RequestedDateNep", Value = model.RequestedDateNep };
                var LetterNumberParam = new SqlParameter { ParameterName = "@LetterNumber", Value = model.LetterNumber == null ? string.Empty : model.LetterNumber };
                var FYIDParam = new SqlParameter { ParameterName = "@FYID", Value = model.FYID };
                var TotalAmountParam = new SqlParameter { ParameterName = "@TotalAmount", Value = model.TotalAmount };
                var RemarksParam = new SqlParameter { ParameterName = "@Remarks", Value = model.Remarks == null ? string.Empty : model.Remarks };
                var ResponsiblePersonNameParam = new SqlParameter { ParameterName = "@ResponsiblePersonName", Value = model.ResponsiblePersonName == null ? string.Empty : model.ResponsiblePersonName };
                var PostParam = new SqlParameter { ParameterName = "@Post", Value = model.Post == null ? string.Empty : model.Post };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var UploadedDocParam = new SqlParameter { ParameterName = "@UploadedDoc", Value = model.UploadedDoc == null ? string.Empty : model.UploadedDoc };
                var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@InternalBerujuId", Value = model.InternalBerujuId };
                var ToWhomofficeIdParam = new SqlParameter { ParameterName = "@ToWhomofficeId", Value = model.ToWhomofficeId };
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


                var result = db.Database.ExecuteSqlCommand("exec IN_InsertInternalSamparikshadReqMaster @ToWhomMinistryName,@ToWhomDeptName,@ToWhomOfficeName,@OfficeAddress,@RequestedDateEng,@RequestedDateNep,@LetterNumber,@FYID,@TotalAmount,@Remarks,@ResponsiblePersonName,@Post,@OfficeId,@UploadedDoc,@InternalBerujuId,@ToWhomofficeId,@Message OUT,@PrimaryId OUT",
                        ToWhomMinistryNameParam, ToWhomDeptNameParam, ToWhomOfficeNameParam, OfficeAddressParam, RequestedDateEngParam, RequestedDateNepParam, LetterNumberParam, FYIDParam, TotalAmountParam, RemarksParam, ResponsiblePersonNameParam, PostParam, OfficeIdParam, UploadedDocParam, ExternalBerujuIdParam, ToWhomofficeIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                if (PKID > 0)
                {
                    model.ObjInternalSamparikshadReqDetailViewModel = new InternalSamparikshadReqDetailViewModel();
                    var MasterIdParam = new SqlParameter { ParameterName = "@MasterId", Value = PKID };
                    var InternalOrExteranlBerujuIdParam = new SqlParameter { ParameterName = "@InternalOrExteranlBerujuId", Value = model.InternalBerujuId };
                    var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = 2 };
                    var BerujuDafaNumberParam = new SqlParameter { ParameterName = "@BerujuDafaNumber", Value = model.BerujuDafaNumber == null ? string.Empty : model.BerujuDafaNumber };
                    var BerujuShortDesParam = new SqlParameter { ParameterName = "@BerujuShortDes", Value = model.BerujuShortDescription == null ? string.Empty : model.BerujuShortDescription };
                    var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = model.TotalAmount };

                    var MessageParamDetail = new SqlParameter
                    {
                        ParameterName = "@MessageDetail",
                        DbType = DbType.String,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };

                    try
                    {
                        var insertRequestDetails = db.Database.ExecuteSqlCommand("exec IN_InsertInternalSamparikshadReqDetail @MasterId,@InternalOrExteranlBerujuId,@InternalOrExternal,@BerujuDafaNumber,@BerujuShortDes,@BerujuAmount,@MessageDetail OUT",
                        MasterIdParam, InternalOrExteranlBerujuIdParam, InternalOrExternalParam, BerujuDafaNumberParam, BerujuShortDesParam, BerujuAmountParam, MessageParamDetail);

                    }
                    catch (Exception e)
                    {
                        string error = e.ToString();

                    }
                    //delete data from samparikshad req to whom details...
                    //var DeleteresultToWhomDetails = db.Database.ExecuteSqlCommand("exec SPDeleteSamparikshadReqMasterDetail @DelExternalBerujuId,@DelOfficeId,@DelMessage OUT", DelExternalBerujuIdParam, DelOfficeIdParam, DelMessageParam);


                    foreach (var item in model.InternalSamparikshadTowhomDetailVMList)
                    {
                        var SMSamparikshadIdParam = new SqlParameter { ParameterName = "@InternalSMSamparikshadId", Value = PKID };
                        var SMExternalBerujuIdParam = new SqlParameter { ParameterName = "@SMInternalBerujuId", Value = model.InternalBerujuId };
                        var EBToWhomIdParam = new SqlParameter { ParameterName = "@IBToWhomId", Value = item.IBToWhomId };
                        var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.PersonName == null ? string.Empty : item.PersonName };
                        var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                        var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };
                        var BerujuAmountReqestParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = item.BerujuAmount };
                        var RevisedAmountParam = new SqlParameter { ParameterName = "@RevisedAmount", Value = item.RevisedAmount.HasValue ? item.RevisedAmount : 0 };
                        var SamparikshadDateParam = new SqlParameter { ParameterName = "@SamparikshadDate", Value = model.RequestedDateEng };
                        var SMOfficeIdParam = new SqlParameter { ParameterName = "@SMOfficeId", Value = item.OfficeId };


                        var SMMessageParam = new SqlParameter
                        {
                            ParameterName = "@SMMessage",
                            DbType = DbType.String,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output
                        };

                        var SMresult = db.Database.ExecuteSqlCommand("exec IN_InsertInternalSamparikshadReqToWhomDetails @InternalSMSamparikshadId,@SMInternalBerujuId,@IBToWhomId,@PersonName,@PanNumber,@MobielNumber,@BerujuAmount,@RevisedAmount,@SamparikshadDate,@SMOfficeId,@SMMessage OUT",
                       SMSamparikshadIdParam, SMExternalBerujuIdParam, EBToWhomIdParam, PersonNameParam, PanNumberParam, MobielNumberParam, BerujuAmountReqestParam, RevisedAmountParam, SamparikshadDateParam, SMOfficeIdParam, SMMessageParam);

                    }




                    //if (model.SamparikshadReqDetailViewModelList.Count > 0)
                    //{


                    //    foreach (var item in model.SamparikshadReqDetailViewModelList)
                    //    {
                    //        var MasterIdParam = new SqlParameter { ParameterName = "@MasterId", Value = PKID };
                    //        var InternalOrExteranlBerujuIdParam = new SqlParameter { ParameterName = "@InternalOrExteranlBerujuId", Value = item.InternalOrExteranlBerujuId };
                    //        var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = item.InternalOrExternal };
                    //        var BerujuDafaNumberParam = new SqlParameter { ParameterName = "@BerujuDafaNumber", Value = item.BerujuDafaNumber};
                    //        var BerujuShortDesParam = new SqlParameter { ParameterName = "@BerujuShortDes", Value = item.BerujuShortDes == null ? string.Empty : item.BerujuShortDes };
                    //        var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = item.BerujuAmount };


                    //        var MessageParamDetail = new SqlParameter
                    //        {
                    //            ParameterName = "@MessageDetail",
                    //            DbType = DbType.String,
                    //            Size = 50,
                    //            Direction = System.Data.ParameterDirection.Output
                    //        };

                    //        var resultToWhom = db.Database.ExecuteSqlCommand("exec InsertSamparikshadReqDetail @MasterId,@InternalOrExteranlBerujuId,@InternalOrExternal,@BerujuDafaNumber,@BerujuShortDes,@BerujuAmount,@MessageDetail OUT", 
                    //            MasterIdParam, InternalOrExteranlBerujuIdParam, InternalOrExternalParam, BerujuDafaNumberParam, BerujuShortDesParam, BerujuAmountParam, MessageParamDetail);



                    //    }
                    //}
                }



                return rms;

            }

        }




        public ReturnMessageViewModel UpdateSamparikshadReqDetail(SamparikshadReqMasterViewModel model)
        {

            using (BerujuEntities db = new BerujuEntities())
            {
                model.OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
                var DelExternalBerujuIdParam = new SqlParameter { ParameterName = "@DelExternalBerujuId", Value = model.ExternalBerujuId };
                var DelOfficeIdParam = new SqlParameter { ParameterName = "@DelOfficeId", Value = model.OfficeId };
                var DelMessageParam = new SqlParameter
                {
                    ParameterName = "@DelMessage",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                //var Deleteresult = db.Database.ExecuteSqlCommand("exec SPDeleteSamparikshadReqMasterDetail @DelExternalBerujuId,@DelOfficeId,@DelMessage OUT", DelExternalBerujuIdParam, DelOfficeIdParam, DelMessageParam);

                //model.RequestedDateEng = DateTime.Now;
                //model.RequestedDateNep = Utilities.GetNepaliDateFromEng(model.RequestedDateEng);


                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var SamparikshadReqMasterIdParam = new SqlParameter { ParameterName = "@SamparikshadReqMasterId", Value = model.SamparikshadReqMasterId };
                var ToWhomMinistryNameParam = new SqlParameter { ParameterName = "@ToWhomMinistryName", Value = model.ToWhomMinistryName == null ? string.Empty : model.ToWhomMinistryName };
                var ToWhomDeptNameParam = new SqlParameter { ParameterName = "@ToWhomDeptName", Value = model.ToWhomDeptName == null ? string.Empty : model.ToWhomDeptName };
                var ToWhomOfficeNameParam = new SqlParameter { ParameterName = "@ToWhomOfficeName", Value = model.ToWhomOfficeName == null ? string.Empty : model.ToWhomOfficeName };
                var OfficeAddressParam = new SqlParameter { ParameterName = "@OfficeAddress", Value = model.OfficeAddress == null ? string.Empty : model.OfficeAddress };
                var RequestedDateEngParam = new SqlParameter { ParameterName = "@RequestedDateEng", Value = model.RequestedDateEng };
                var RequestedDateNepParam = new SqlParameter { ParameterName = "@RequestedDateNep", Value = model.RequestedDateNep };
                var LetterNumberParam = new SqlParameter { ParameterName = "@LetterNumber", Value = model.LetterNumber == null ? string.Empty : model.LetterNumber };
                var FYIDParam = new SqlParameter { ParameterName = "@FYID", Value = model.FYID };
                var TotalAmountParam = new SqlParameter { ParameterName = "@TotalAmount", Value = model.TotalAmount };
                var RemarksParam = new SqlParameter { ParameterName = "@Remarks", Value = model.Remarks == null ? string.Empty : model.Remarks };
                var ResponsiblePersonNameParam = new SqlParameter { ParameterName = "@ResponsiblePersonName", Value = model.ResponsiblePersonName == null ? string.Empty : model.ResponsiblePersonName };
                var PostParam = new SqlParameter { ParameterName = "@Post", Value = model.Post == null ? string.Empty : model.Post };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var UploadedDocParam = new SqlParameter { ParameterName = "@UploadedDoc", Value = model.UploadedDoc == null ? string.Empty : model.UploadedDoc };
                var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = model.ExternalBerujuId };
                var ToWhomofficeIdParam = new SqlParameter { ParameterName = "@ToWhomofficeId", Value = model.ToWhomofficeId };
                var RemarksForRequestParam = new SqlParameter { ParameterName = "@RemarksForRequest", Value = model.RemarksForRequest == null ? string.Empty : model.RemarksForRequest };
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


                var result = db.Database.ExecuteSqlCommand("exec UpdateSamparikshadReqMaster @SamparikshadReqMasterId,@ToWhomMinistryName,@ToWhomDeptName,@ToWhomOfficeName,@OfficeAddress,@RequestedDateEng,@RequestedDateNep,@LetterNumber,@FYID,@TotalAmount,@Remarks,@ResponsiblePersonName,@Post,@OfficeId,@UploadedDoc,@ExternalBerujuId,@ToWhomofficeId,@RemarksForRequest,@Message OUT,@PrimaryId OUT",
                        SamparikshadReqMasterIdParam, ToWhomMinistryNameParam, ToWhomDeptNameParam, ToWhomOfficeNameParam, OfficeAddressParam, RequestedDateEngParam, RequestedDateNepParam, LetterNumberParam, FYIDParam, TotalAmountParam, RemarksParam, ResponsiblePersonNameParam, PostParam, OfficeIdParam, UploadedDocParam, ExternalBerujuIdParam, ToWhomofficeIdParam,RemarksForRequestParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                if (PKID > 0)
                {
                    model.ObjSamparikshadReqDetailViewModel = new SamparikshadReqDetailViewModel();

                    var MasterIdParam = new SqlParameter { ParameterName = "@MasterId", Value = PKID };
                    var InternalOrExteranlBerujuIdParam = new SqlParameter { ParameterName = "@InternalOrExteranlBerujuId", Value = model.ExternalBerujuId };
                    var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = 2 };
                    var BerujuDafaNumberParam = new SqlParameter { ParameterName = "@BerujuDafaNumber", Value = model.BerujuDafaNumber == null ? string.Empty : model.BerujuDafaNumber };
                    var BerujuShortDesParam = new SqlParameter { ParameterName = "@BerujuShortDes", Value = model.BerujuShortDescription == null ? string.Empty : model.BerujuShortDescription };
                    var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = model.TotalAmount };

                    var MessageParamDetail = new SqlParameter
                    {
                        ParameterName = "@MessageDetail",
                        DbType = DbType.String,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };

                    try
                    {
                        var insertRequestDetails = db.Database.ExecuteSqlCommand("exec UpdateSamparikshadReqDetail @MasterId,@InternalOrExteranlBerujuId,@InternalOrExternal,@BerujuDafaNumber,@BerujuShortDes,@BerujuAmount,@MessageDetail OUT",
                        MasterIdParam, InternalOrExteranlBerujuIdParam, InternalOrExternalParam, BerujuDafaNumberParam, BerujuShortDesParam, BerujuAmountParam, MessageParamDetail);

                    }
                    catch (Exception e)
                    {
                        string error = e.ToString();

                    }
                    //delete data from samparikshad req to whom details...
                    //var DeleteresultToWhomDetails = db.Database.ExecuteSqlCommand("exec SPDeleteSamparikshadReqMasterDetail @DelExternalBerujuId,@DelOfficeId,@DelMessage OUT", DelExternalBerujuIdParam, DelOfficeIdParam, DelMessageParam);

                    var DelSamparikshadIdRequestParam = new SqlParameter { ParameterName = "@DelSamparikshadId", Value = PKID };
                    var DelExternalBerujuIdRequestParam = new SqlParameter { ParameterName = "@DelExternalBerujuId", Value = model.ExternalBerujuId };

                    //First delete
                    var DelMessageParamRequest = new SqlParameter
                    {
                        ParameterName = "@DelMessage",
                        DbType = DbType.String,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };


                    var Delresult = db.Database.ExecuteSqlCommand("exec DeleteSamparikshadRequestToWhomDetail @DelSamparikshadId,@DelExternalBerujuId,@DelMessage OUT", DelSamparikshadIdRequestParam, DelExternalBerujuIdRequestParam, DelMessageParam);
                    string DelMessage = DelMessageParam.SqlValue.ToString();



                    foreach (var item in model.SamparikshadTowhomDetailVMList)
                    {
                        string FileNameVal = item.SupportingDocFiles == null ? string.Empty : item.SupportingDocFiles.FileName;
                        if (!string.IsNullOrEmpty(FileNameVal))
                        {
                            string concateletter = "S-R-" + item.SMTowhomDetailId + item.ExternalBerujuId;
                            item.UploadedFileUrl = concateletter + "_" + item.SupportingDocFiles.FileName;
                        }
                        else
                        {
                            item.UploadedFileUrl = string.Empty;
                        }

                        var SMSamparikshadIdParam = new SqlParameter { ParameterName = "@SMSamparikshadId", Value = PKID };
                        var SMExternalBerujuIdParam = new SqlParameter { ParameterName = "@SMExternalBerujuId", Value = model.ExternalBerujuId };
                        var EBToWhomIdParam = new SqlParameter { ParameterName = "@EBToWhomId", Value = item.EBToWhomId };
                        var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.PersonName == null ? string.Empty : item.PersonName };
                        var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                        var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };
                        var BerujuAmountReqestParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = item.IndividualAmount.HasValue ? item.IndividualAmount : 0 };
                        var RevisedAmountParam = new SqlParameter { ParameterName = "@RevisedAmount", Value = item.RevisedAmount.HasValue ? item.RevisedAmount : 0 };
                        var SamparikshadDateParam = new SqlParameter { ParameterName = "@SamparikshadDate", Value = model.RequestedDateEng };
                        var SMOfficeIdParam = new SqlParameter { ParameterName = "@SMOfficeId", Value = item.OfficeId };
                        var UploadedFileUrlParam = new SqlParameter { ParameterName = "@UploadedFileUrl", Value = item.UploadedFileUrl };

                        var SMMessageParam = new SqlParameter
                        {
                            ParameterName = "@SMMessage",
                            DbType = DbType.String,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output
                        };

                        var SMresult = db.Database.ExecuteSqlCommand("exec InsertSamparikshadReqToWhomDetails @SMSamparikshadId,@SMExternalBerujuId,@EBToWhomId,@PersonName,@PanNumber,@MobielNumber,@BerujuAmount,@RevisedAmount,@SamparikshadDate,@SMOfficeId,@UploadedFileUrl,@SMMessage OUT",
                         SMSamparikshadIdParam, SMExternalBerujuIdParam, EBToWhomIdParam, PersonNameParam, PanNumberParam, MobielNumberParam, BerujuAmountReqestParam, RevisedAmountParam, SamparikshadDateParam, SMOfficeIdParam, UploadedFileUrlParam, SMMessageParam);

                    }


                }
                

                return rms;

            }

        }

        public ReturnMessageViewModel UpdateInternalSamparikshadReqDetail(InternalSamparikshadReqMasterViewModel model)
        {

            using (BerujuEntities db = new BerujuEntities())
            {
                model.OfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
                var DelExternalBerujuIdParam = new SqlParameter { ParameterName = "@DelExternalBerujuId", Value = model.InternalBerujuId };
                var DelOfficeIdParam = new SqlParameter { ParameterName = "@DelOfficeId", Value = model.OfficeId };
                var DelMessageParam = new SqlParameter
                {
                    ParameterName = "@DelMessage",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                //var Deleteresult = db.Database.ExecuteSqlCommand("exec SPDeleteSamparikshadReqMasterDetail @DelExternalBerujuId,@DelOfficeId,@DelMessage OUT", DelExternalBerujuIdParam, DelOfficeIdParam, DelMessageParam);

                //model.RequestedDateEng = DateTime.Now;
                //model.RequestedDateNep = Utilities.GetNepaliDateFromEng(model.RequestedDateEng);


                ReturnMessageViewModel rms = new ReturnMessageViewModel();
                var SamparikshadReqMasterIdParam = new SqlParameter { ParameterName = "@InternalSamparikshadReqMasterId", Value = model.InternalSamparikshadReqMasterId };
                var ToWhomMinistryNameParam = new SqlParameter { ParameterName = "@ToWhomMinistryName", Value = model.ToWhomMinistryName == null ? string.Empty : model.ToWhomMinistryName };
                var ToWhomDeptNameParam = new SqlParameter { ParameterName = "@ToWhomDeptName", Value = model.ToWhomDeptName == null ? string.Empty : model.ToWhomDeptName };
                var ToWhomOfficeNameParam = new SqlParameter { ParameterName = "@ToWhomOfficeName", Value = model.ToWhomOfficeName == null ? string.Empty : model.ToWhomOfficeName };
                var OfficeAddressParam = new SqlParameter { ParameterName = "@OfficeAddress", Value = model.OfficeAddress == null ? string.Empty : model.OfficeAddress };
                var RequestedDateEngParam = new SqlParameter { ParameterName = "@RequestedDateEng", Value = model.RequestedDateEng };
                var RequestedDateNepParam = new SqlParameter { ParameterName = "@RequestedDateNep", Value = model.RequestedDateNep };
                var LetterNumberParam = new SqlParameter { ParameterName = "@LetterNumber", Value = model.LetterNumber == null ? string.Empty : model.LetterNumber };
                var FYIDParam = new SqlParameter { ParameterName = "@FYID", Value = model.FYID };
                var TotalAmountParam = new SqlParameter { ParameterName = "@TotalAmount", Value = model.TotalAmount };
                var RemarksParam = new SqlParameter { ParameterName = "@Remarks", Value = model.Remarks == null ? string.Empty : model.Remarks };
                var ResponsiblePersonNameParam = new SqlParameter { ParameterName = "@ResponsiblePersonName", Value = model.ResponsiblePersonName == null ? string.Empty : model.ResponsiblePersonName };
                var PostParam = new SqlParameter { ParameterName = "@Post", Value = model.Post == null ? string.Empty : model.Post };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var UploadedDocParam = new SqlParameter { ParameterName = "@UploadedDoc", Value = model.UploadedDoc == null ? string.Empty : model.UploadedDoc };
                var ExternalBerujuIdParam = new SqlParameter { ParameterName = "@ExternalBerujuId", Value = model.InternalBerujuId };
                var ToWhomofficeIdParam = new SqlParameter { ParameterName = "@ToWhomofficeId", Value = model.ToWhomofficeId };
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


                var result = db.Database.ExecuteSqlCommand("exec IN_UpdateInternalSamparikshadReqMaster @InternalSamparikshadReqMasterId,@ToWhomMinistryName,@ToWhomDeptName,@ToWhomOfficeName,@OfficeAddress,@RequestedDateEng,@RequestedDateNep,@LetterNumber,@FYID,@TotalAmount,@Remarks,@ResponsiblePersonName,@Post,@OfficeId,@UploadedDoc,@InternalBerujuId,@ToWhomofficeId,@Message OUT,@PrimaryId OUT",
                        SamparikshadReqMasterIdParam, ToWhomMinistryNameParam, ToWhomDeptNameParam, ToWhomOfficeNameParam, OfficeAddressParam, RequestedDateEngParam, RequestedDateNepParam, LetterNumberParam, FYIDParam, TotalAmountParam, RemarksParam, ResponsiblePersonNameParam, PostParam, OfficeIdParam, UploadedDocParam, ExternalBerujuIdParam, ToWhomofficeIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                rms.ReturnMessage = MessageParam.SqlValue.ToString();
                rms.PrimaryId = PKID;
                if (PKID > 0)
                {
                    model.ObjInternalSamparikshadReqDetailViewModel = new InternalSamparikshadReqDetailViewModel();

                    var MasterIdParam = new SqlParameter { ParameterName = "@MasterId", Value = PKID };
                    var InternalOrExteranlBerujuIdParam = new SqlParameter { ParameterName = "@InternalOrExteranlBerujuId", Value = model.InternalBerujuId };
                    var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = 2 };
                    var BerujuDafaNumberParam = new SqlParameter { ParameterName = "@BerujuDafaNumber", Value = model.BerujuDafaNumber == null ? string.Empty : model.BerujuDafaNumber };
                    var BerujuShortDesParam = new SqlParameter { ParameterName = "@BerujuShortDes", Value = model.BerujuShortDescription == null ? string.Empty : model.BerujuShortDescription };
                    var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = model.TotalAmount };

                    var MessageParamDetail = new SqlParameter
                    {
                        ParameterName = "@MessageDetail",
                        DbType = DbType.String,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };

                    try
                    {
                        var insertRequestDetails = db.Database.ExecuteSqlCommand("exec IN_UpdateInternalSamparikshadReqDetail @MasterId,@InternalOrExteranlBerujuId,@InternalOrExternal,@BerujuDafaNumber,@BerujuShortDes,@BerujuAmount,@MessageDetail OUT",
                        MasterIdParam, InternalOrExteranlBerujuIdParam, InternalOrExternalParam, BerujuDafaNumberParam, BerujuShortDesParam, BerujuAmountParam, MessageParamDetail);

                    }
                    catch (Exception e)
                    {
                        string error = e.ToString();

                    }
                    //delete data from samparikshad req to whom details...
                    //var DeleteresultToWhomDetails = db.Database.ExecuteSqlCommand("exec SPDeleteSamparikshadReqMasterDetail @DelExternalBerujuId,@DelOfficeId,@DelMessage OUT", DelExternalBerujuIdParam, DelOfficeIdParam, DelMessageParam);

                    var DelSamparikshadIdRequestParam = new SqlParameter { ParameterName = "@DelSamparikshadId", Value = PKID };
                    var DelExternalBerujuIdRequestParam = new SqlParameter { ParameterName = "@DelInternalBerujuId", Value = model.InternalBerujuId };

                    //First delete
                    var DelMessageParamRequest = new SqlParameter
                    {
                        ParameterName = "@DelMessage",
                        DbType = DbType.String,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };


                    var Delresult = db.Database.ExecuteSqlCommand("exec IN_DeleteSamparikshadRequestToWhomDetail @DelSamparikshadId,@DelInternalBerujuId,@DelMessage OUT", DelSamparikshadIdRequestParam, DelExternalBerujuIdRequestParam, DelMessageParam);
                    string DelMessage = DelMessageParam.SqlValue.ToString();

                    foreach (var item in model.InternalSamparikshadTowhomDetailVMList)
                    {
                        var SMSamparikshadIdParam = new SqlParameter { ParameterName = "@InternalSMSamparikshadId", Value = PKID };
                        var SMExternalBerujuIdParam = new SqlParameter { ParameterName = "@SMInternalBerujuId", Value = model.InternalBerujuId };
                        var EBToWhomIdParam = new SqlParameter { ParameterName = "@IBToWhomId", Value = item.IBToWhomId };
                        var PersonNameParam = new SqlParameter { ParameterName = "@PersonName", Value = item.PersonName == null ? string.Empty : item.PersonName };
                        var PanNumberParam = new SqlParameter { ParameterName = "@PanNumber", Value = item.PanNumber == null ? string.Empty : item.PanNumber };
                        var MobielNumberParam = new SqlParameter { ParameterName = "@MobielNumber", Value = item.MobielNumber == null ? string.Empty : item.MobielNumber };
                        var BerujuAmountReqestParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = item.IndividualAmount.HasValue ? item.IndividualAmount : 0 };
                        var RevisedAmountParam = new SqlParameter { ParameterName = "@RevisedAmount", Value = item.RevisedAmount.HasValue ? item.RevisedAmount : 0 };
                        var SamparikshadDateParam = new SqlParameter { ParameterName = "@SamparikshadDate", Value = model.RequestedDateEng };
                        var SMOfficeIdParam = new SqlParameter { ParameterName = "@SMOfficeId", Value = item.OfficeId };


                        var SMMessageParam = new SqlParameter
                        {
                            ParameterName = "@SMMessage",
                            DbType = DbType.String,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output
                        };

                        var SMresult = db.Database.ExecuteSqlCommand("exec IN_InsertInternalSamparikshadReqToWhomDetails @InternalSMSamparikshadId,@SMInternalBerujuId,@IBToWhomId,@PersonName,@PanNumber,@MobielNumber,@BerujuAmount,@RevisedAmount,@SamparikshadDate,@SMOfficeId,@SMMessage OUT",
                       SMSamparikshadIdParam, SMExternalBerujuIdParam, EBToWhomIdParam, PersonNameParam, PanNumberParam, MobielNumberParam, BerujuAmountReqestParam, RevisedAmountParam, SamparikshadDateParam, SMOfficeIdParam, SMMessageParam);

                    }




                    //if (model.SamparikshadReqDetailViewModelList.Count > 0)
                    //{


                    //    foreach (var item in model.SamparikshadReqDetailViewModelList)
                    //    {
                    //        var MasterIdParam = new SqlParameter { ParameterName = "@MasterId", Value = PKID };
                    //        var InternalOrExteranlBerujuIdParam = new SqlParameter { ParameterName = "@InternalOrExteranlBerujuId", Value = item.InternalOrExteranlBerujuId };
                    //        var InternalOrExternalParam = new SqlParameter { ParameterName = "@InternalOrExternal", Value = item.InternalOrExternal };
                    //        var BerujuDafaNumberParam = new SqlParameter { ParameterName = "@BerujuDafaNumber", Value = item.BerujuDafaNumber};
                    //        var BerujuShortDesParam = new SqlParameter { ParameterName = "@BerujuShortDes", Value = item.BerujuShortDes == null ? string.Empty : item.BerujuShortDes };
                    //        var BerujuAmountParam = new SqlParameter { ParameterName = "@BerujuAmount", Value = item.BerujuAmount };


                    //        var MessageParamDetail = new SqlParameter
                    //        {
                    //            ParameterName = "@MessageDetail",
                    //            DbType = DbType.String,
                    //            Size = 50,
                    //            Direction = System.Data.ParameterDirection.Output
                    //        };

                    //        var resultToWhom = db.Database.ExecuteSqlCommand("exec InsertSamparikshadReqDetail @MasterId,@InternalOrExteranlBerujuId,@InternalOrExternal,@BerujuDafaNumber,@BerujuShortDes,@BerujuAmount,@MessageDetail OUT", 
                    //            MasterIdParam, InternalOrExteranlBerujuIdParam, InternalOrExternalParam, BerujuDafaNumberParam, BerujuShortDesParam, BerujuAmountParam, MessageParamDetail);



                    //    }
                    //}
                }



                return rms;

            }

        }





        public SamparikshadRequestMaterDetailVM SPGetSamparikshadRequestletter(int OfficeId, int ExternalBerujuID)
        {
            using (BerujuEntities db = new BerujuEntities())
            {

                SamparikshadRequestMaterDetailVM retrunModel = new SamparikshadRequestMaterDetailVM();
                retrunModel = db.Database.SqlQuery<SamparikshadRequestMaterDetailVM>("SPGetSamparikshadRequestletter {0},{1}", OfficeId, ExternalBerujuID).FirstOrDefault();
                if (retrunModel == null)
                {
                    retrunModel = new SamparikshadRequestMaterDetailVM();
                    retrunModel.OfficeId = OfficeId;

                }
                return retrunModel;
            }
        }

        public SamparikshadRequestMaterDetailVM SPGetSamparikshadRequestletterByPrimaryId(int OfficeId, int PrimaryId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {

                SamparikshadRequestMaterDetailVM retrunModel = new SamparikshadRequestMaterDetailVM();
                retrunModel = db.Database.SqlQuery<SamparikshadRequestMaterDetailVM>("SPGetSamparikshadRequestletterByPrimaryId {0},{1}", OfficeId, PrimaryId).FirstOrDefault();
                if (retrunModel == null)
                {
                    retrunModel = new SamparikshadRequestMaterDetailVM();
                }
                return retrunModel;
            }
        }


        public InternalSamparikshadRequestMaterDetailVM IN_SPGetInternalSamparikshadRequestletter(int OfficeId, int InternalBerujuId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {

                InternalSamparikshadRequestMaterDetailVM retrunModel = new InternalSamparikshadRequestMaterDetailVM();
                retrunModel = db.Database.SqlQuery<InternalSamparikshadRequestMaterDetailVM>("IN_SPGetInternalSamparikshadRequestletter {0},{1}", OfficeId, InternalBerujuId).FirstOrDefault();
                if (retrunModel == null)
                {
                    retrunModel = new InternalSamparikshadRequestMaterDetailVM();
                    retrunModel.OfficeId = OfficeId;

                }
                return retrunModel;
            }
        }

        public InternalSamparikshadRequestMaterDetailVM IN_SPGetInternalSamparikshadRequestletterByPrimaryId(int OfficeId, int PrimaryId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {

                InternalSamparikshadRequestMaterDetailVM retrunModel = new InternalSamparikshadRequestMaterDetailVM();
                retrunModel = db.Database.SqlQuery<InternalSamparikshadRequestMaterDetailVM>("IN_SPGetInternalSamparikshadRequestletterByPrimaryId {0},{1}", OfficeId, PrimaryId).FirstOrDefault();
                if (retrunModel == null)
                {
                    retrunModel = new InternalSamparikshadRequestMaterDetailVM();
                }
                return retrunModel;
            }
        }




        public int CheckIfAlreadyInsertedIntoSamparikshad(int OfficeId, int ExternalBerujuId)
        {
            int returnCount = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {

                    returnCount = db.Database.SqlQuery<int>(@"select count(*) as TotalCount From SamparishadDetail where OfficeId='" + OfficeId + "' and ExternalBerujuId='" + ExternalBerujuId + "'").FirstOrDefault();
                }

                catch (Exception)
                {

                    returnCount = 0;
                }


            }

            return returnCount;
        }



        public int CheckIfAlreadyRequestedForInernalSamparikshad(int OfficeId, int InternalBerujuId)
        {
            int returnCount = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {

                    returnCount = db.Database.SqlQuery<int>(@"select count(*) as Total From InternalSamparikshadReqMaster
where InternalBerujuId='" + InternalBerujuId + "' and OfficeId='" + OfficeId + "'").FirstOrDefault();
                }

                catch (Exception)
                {

                    returnCount = 0;
                }


            }

            return returnCount;
        }


        public int CheckIfAlreadyRequestedForSamparikshad(int OfficeId, int ExternalBerujuId)
        {
            int returnCount = 0;
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {

                    returnCount = db.Database.SqlQuery<int>(@"select count(*) as Total From SamparikshadReqMaster
where ExternalBerujuId='" + ExternalBerujuId + "' and OfficeId='" + OfficeId + "'").FirstOrDefault();
                }

                catch (Exception)
                {

                    returnCount = 0;
                }


            }

            return returnCount;
        }

        public InternalSamparikshadReqMasterViewModel IN_SPGetInternalSamparikshadRequestDetailByEBID(int PrimaryId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                InternalSamparikshadReqMasterViewModel ReturnModel = new InternalSamparikshadReqMasterViewModel();

                ReturnModel = db.Database.SqlQuery<InternalSamparikshadReqMasterViewModel>("IN_SPGetInternalSamparikshadRequestDetailByEBID {0}", PrimaryId).FirstOrDefault();
                if (ReturnModel == null)
                {
                    ReturnModel = new InternalSamparikshadReqMasterViewModel();
                }
                return ReturnModel;
            }
        }


        public SamparikshadReqMasterViewModel SPGetSamparikshadRequestDetailByEBID(int PrimaryId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                SamparikshadReqMasterViewModel ReturnModel = new SamparikshadReqMasterViewModel();

                ReturnModel = db.Database.SqlQuery<SamparikshadReqMasterViewModel>("SPGetSamparikshadRequestDetailByEBID {0}", PrimaryId).FirstOrDefault();
                if (ReturnModel == null)
                {
                    ReturnModel = new SamparikshadReqMasterViewModel();
                }
                return ReturnModel;
            }
        }

        public SamparikshadReqMasterViewModel SPGetSamparikshadRequestDetailByPrimaryId(int ExternalBerujuId, int PrimaryId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                SamparikshadReqMasterViewModel ReturnModel = new SamparikshadReqMasterViewModel();

                ReturnModel = db.Database.SqlQuery<SamparikshadReqMasterViewModel>("SPGetSamparikshadRequestDetailByPrimaryId {0},{1}", ExternalBerujuId, PrimaryId).FirstOrDefault();
                if (ReturnModel == null)
                {
                    ReturnModel = new SamparikshadReqMasterViewModel();
                }
                return ReturnModel;
            }
        }

        public InternalSamparikshadReqMasterViewModel IN_SPGetInternalSamparikshadRequestDetailByPrimaryId(int InternalBerujuId, int PrimaryId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                InternalSamparikshadReqMasterViewModel ReturnModel = new InternalSamparikshadReqMasterViewModel();

                ReturnModel = db.Database.SqlQuery<InternalSamparikshadReqMasterViewModel>("IN_SPGetInternalSamparikshadRequestDetailByPrimaryId {0},{1}", InternalBerujuId, PrimaryId).FirstOrDefault();
                if (ReturnModel == null)
                {
                    ReturnModel = new InternalSamparikshadReqMasterViewModel();
                }
                return ReturnModel;
            }
        }



        public ReturnMessageViewModel SPDeleteSamparikshadReqMasterDetail(int OfficeId, int ExternalBerujuId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

                var DelExternalBerujuIdParam = new SqlParameter { ParameterName = "@DelExternalBerujuId", Value = ExternalBerujuId };
                var DelOfficeIdParam = new SqlParameter { ParameterName = "@DelOfficeId", Value = OfficeId };

                var DelMessageParam = new SqlParameter
                {
                    ParameterName = "@DelMessage",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                var result = db.Database.ExecuteSqlCommand("exec SPDeleteSamparikshadReqMasterDetail @DelExternalBerujuId,@DelOfficeId,@DelMessage OUT", DelExternalBerujuIdParam, DelOfficeIdParam, DelMessageParam);
                returnModel.ReturnMessage = DelMessageParam.SqlValue.ToString();
                returnModel.PrimaryId = 0;
                return returnModel;
            }

        }

        public List<GetsamparikshadrequesttowhomforletterViewModel> GetsamparikshadrequesttowhomforletterListForLetter(int letterId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<GetsamparikshadrequesttowhomforletterViewModel> ReturnList = new List<GetsamparikshadrequesttowhomforletterViewModel>();

                ReturnList = db.Database.SqlQuery<GetsamparikshadrequesttowhomforletterViewModel>("Getsamparikshadrequesttowhomforletter {0}", letterId).ToList();
                return ReturnList;
            }
        }

        public List<GetInternalsamparikshadrequesttowhomforletterViewModel> IN_Getsamparikshadrequesttowhomforletter(int letterId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<GetInternalsamparikshadrequesttowhomforletterViewModel> ReturnList = new List<GetInternalsamparikshadrequesttowhomforletterViewModel>();

                ReturnList = db.Database.SqlQuery<GetInternalsamparikshadrequesttowhomforletterViewModel>("IN_Getsamparikshadrequesttowhomforletter {0}", letterId).ToList();
                return ReturnList;
            }
        }





        public List<InternalBeruju> ListInternalBerujuForSamparikshadRequestMake(int OfficeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<InternalBeruju> ReturnList = new List<InternalBeruju>();

                ReturnList = db.Database.SqlQuery<InternalBeruju>("IN_GetInternalBerujulistForSamparikshadRequestMake {0}", OfficeId).ToList();
                return ReturnList;
            }
        }


        public decimal IN_GetInternalSamparikshadRemainingAmountForRequest(int InternalBerujuId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                decimal RemainingAmount = 0;
                RemainingAmount = db.Database.SqlQuery<decimal>("IN_GetInternalSamparikshadRemainingAmountForRequest {0}", InternalBerujuId).FirstOrDefault();
                return RemainingAmount;

            }
        }





    }
}