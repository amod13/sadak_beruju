using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4pix_Beruju.Models;
using _4pix_Beruju.Areas.LocalLevel.Models;
using System.Data.SqlClient;
using System.Data;

namespace _4pix_Beruju.Services
{
    public class BudgetSubTitleService
    {

        public List<BudgetSubTitleSetup> GetBudgetSubTitleListByOfficeId(int OfficeId)
        {
            List<BudgetSubTitleSetup> ReturnList = new List<BudgetSubTitleSetup>();
            using (BerujuEntities db = new BerujuEntities())
            {
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                ReturnList = db.Database.SqlQuery<BudgetSubTitleSetup>("GetBudgetSubTitleListByOfficeId @OfficeId", OfficeIdParam).ToList();
                return ReturnList;
            }
        }

        public ReturnMessageViewModel InsertBudgetSubTitleDetails(BudgetSubTitleSetup model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

                var SubTitleCodeParam = new SqlParameter { ParameterName = "@SubTitleCode", Value = model.SubTitleCode };
                var SubTitleNameParam = new SqlParameter { ParameterName = "@SubTitleName", Value = model.SubTitleName };
                var SubTitleStatusParam = new SqlParameter { ParameterName = "@SubTitleStatus", Value = model.SubTitleStatus };
                var ChaluOrPujigatIdParam = new SqlParameter { ParameterName = "@ChaluOrPujigatId", Value = model.ChaluOrPujigatId };
                var DisplayOrderParam = new SqlParameter { ParameterName = "@DisplayOrder", Value = model.DisplayOrder };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
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
                var result = db.Database.ExecuteSqlCommand("exec InsertBudgetSubTitleDetails @SubTitleCode,@SubTitleName,@SubTitleStatus,@ChaluOrPujigatId,@DisplayOrder,@OfficeId,@FiscalYearId,@Message OUT,@PrimaryId OUT", SubTitleCodeParam, SubTitleNameParam, SubTitleStatusParam, ChaluOrPujigatIdParam, DisplayOrderParam, OfficeIdParam, FiscalYearIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel UpdateBudgetSubTitleDetails(BudgetSubTitleSetup model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var BudgetSubTitleIdParam = new SqlParameter { ParameterName = "@BudgetSubTitleId", Value = model.BudgetSubTitleId };
                var SubTitleCodeParam = new SqlParameter { ParameterName = "@SubTitleCode", Value = model.SubTitleCode };
                var SubTitleNameParam = new SqlParameter { ParameterName = "@SubTitleName", Value = model.SubTitleName };
                var SubTitleStatusParam = new SqlParameter { ParameterName = "@SubTitleStatus", Value = model.SubTitleStatus };
                var ChaluOrPujigatIdParam = new SqlParameter { ParameterName = "@ChaluOrPujigatId", Value = model.ChaluOrPujigatId };
                var DisplayOrderParam = new SqlParameter { ParameterName = "@DisplayOrder", Value = model.DisplayOrder };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                var FiscalYearIdParam = new SqlParameter { ParameterName = "@FiscalYearId", Value = model.FiscalYearId };
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
                var result = db.Database.ExecuteSqlCommand("exec UpdateBudgetSubTitleDetails @BudgetSubTitleId,@SubTitleCode,@SubTitleName,@SubTitleStatus,@ChaluOrPujigatId,@DisplayOrder,@OfficeId,@FiscalYearId,@Message OUT,@PrimaryId OUT", BudgetSubTitleIdParam, SubTitleCodeParam, SubTitleNameParam, SubTitleStatusParam, ChaluOrPujigatIdParam, DisplayOrderParam, OfficeIdParam, FiscalYearIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel DeleteBudgetSubTitleDetails(BudgetSubTitleSetup model)
        {
            ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
            if (model.BudgetSubTitleId > 0)
            {
                string BaushiNubmer = model.BudgetSubTitleId.ToString();
                int IfInTable = CheckIfAlreadyInTables(BaushiNubmer);
                if (IfInTable > 0)
                {
                    returnModel.PrimaryId = 0;
                    returnModel.ReturnMessage = @"यो शिर्षक आन्तरीक, अन्तिम वा सैदान्तिक बेरुजु मा प्रयोग भएको छ ।";
                }
                else
                {
                    using (BerujuEntities db = new BerujuEntities())
                    {

                        var BudgetSubTitleIdParam = new SqlParameter { ParameterName = "@BudgetSubTitleId", Value = model.BudgetSubTitleId };
                        var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                        //string aditPost = model.AuditorPost;
                        var MessageParam = new SqlParameter
                        {
                            ParameterName = "@Message",
                            DbType = DbType.String,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Output
                        };

                        try
                        {
                            var result = db.Database.ExecuteSqlCommand("exec DeleteBudgetSubTitleDetail @BudgetSubTitleId,@OfficeId,@Message OUT", BudgetSubTitleIdParam, OfficeIdParam, MessageParam);
                            returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                            returnModel.PrimaryId = model.BudgetSubTitleId;

                        }
                        catch (Exception)
                        {
                            returnModel.ReturnMessage = "Error Please try again";
                            returnModel.PrimaryId = model.BudgetSubTitleId;

                        }


                    }
                }
            }
            return returnModel;


        }

        //check if already in use or not baushinubmer

        public int CheckIfAlreadyInTables(string BaushiNumber)
        {
            int IfInTableCount = 0;

            using (BerujuEntities db = new BerujuEntities())
            {

                IfInTableCount = db.Database.SqlQuery<int>(@"select count(*) as Total From ExternalBeruju where BudgetSubTitle='" + BaushiNumber + "'").FirstOrDefault();
                IfInTableCount = db.Database.SqlQuery<int>(@"select count(*) as Total From InternalBeruju where BudgetSubTitle='" + BaushiNumber + "'").FirstOrDefault();
                IfInTableCount = db.Database.SqlQuery<int>(@"select count(*) as Total From SaidantikBeruju where BudgetSubTitleId='" + BaushiNumber + "'").FirstOrDefault();

            }
            return IfInTableCount;
        }




        public List<ExpenseTitleSetup> GetExpenseTitleListByOfficeId(int OfficeId)
        {
            List<ExpenseTitleSetup> ReturnList = new List<ExpenseTitleSetup>();
            using (BerujuEntities db = new BerujuEntities())
            {
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                ReturnList = db.Database.SqlQuery<ExpenseTitleSetup>("GetExpenseTitleListByOfficeId @OfficeId", OfficeIdParam).ToList();
                return ReturnList;
            }
        }

        public ReturnMessageViewModel InsertExpneseTitleDetails(ExpenseTitleSetup model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();

                var CodeParam = new SqlParameter { ParameterName = "@Code", Value = model.Code };
                var TitleParam = new SqlParameter { ParameterName = "@TItlle", Value = model.TItlle };
                var ChaluOrPujigatIdParam = new SqlParameter { ParameterName = "@ChaluPujiTypeId", Value = model.ChaluPujiTypeId };
                var PujiStatusParam = new SqlParameter { ParameterName = "@PujiStatus", Value = model.PujiStatus };
                var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = model.KoshTypeId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
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
                var result = db.Database.ExecuteSqlCommand("exec InserChaluOrPujigatDetail @Code,@TItlle,@ChaluPujiTypeId,@PujiStatus,@KoshTypeId,@OfficeId,@Message OUT,@PrimaryId OUT", CodeParam, TitleParam, ChaluOrPujigatIdParam, PujiStatusParam, KoshTypeIdParam, OfficeIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel UpdateExpneseTitleDetails(ExpenseTitleSetup model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var ChaluPujigatIdParam = new SqlParameter { ParameterName = "@ChaluPujigatId", Value = model.ChaluPujigatId };
                var CodeParam = new SqlParameter { ParameterName = "@Code", Value = model.Code };
                var TitleParam = new SqlParameter { ParameterName = "@TItlle", Value = model.TItlle };
                var ChaluOrPujigatTypeIdParam = new SqlParameter { ParameterName = "@ChaluPujiTypeId", Value = model.ChaluPujiTypeId };
                var PujiStatusParam = new SqlParameter { ParameterName = "@PujiStatus", Value = model.PujiStatus };
                var KoshTypeIdParam = new SqlParameter { ParameterName = "@KoshTypeId", Value = model.KoshTypeId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
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
                var result = db.Database.ExecuteSqlCommand("exec UpdateChaluOrPujigatDetail @ChaluPujigatId,@Code,@TItlle,@ChaluPujiTypeId,@PujiStatus,@KoshTypeId,@OfficeId,@Message OUT,@PrimaryId OUT", ChaluPujigatIdParam, CodeParam, TitleParam, ChaluOrPujigatTypeIdParam, PujiStatusParam, KoshTypeIdParam, OfficeIdParam, MessageParam, PrimaryIdParam);
                int PKID = (int)PrimaryIdParam.Value;
                returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                returnModel.PrimaryId = PKID;
                return returnModel;
            }

        }

        public ReturnMessageViewModel DeleteChaluOrPujigatTitleDetail(ExpenseTitleSetup model)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                ReturnMessageViewModel returnModel = new ReturnMessageViewModel();
                var ChaluOrPujigatIdParam = new SqlParameter { ParameterName = "@ChaluOrPujigatId", Value = model.ChaluPujigatId };
                var OfficeIdParam = new SqlParameter { ParameterName = "@OfficeId", Value = model.OfficeId };
                //string aditPost = model.AuditorPost;
                var MessageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    DbType = DbType.String,
                    Size = 50,
                    Direction = System.Data.ParameterDirection.Output
                };

                try
                {
                    var result = db.Database.ExecuteSqlCommand("exec DeleteChaluOrPujigatTitleDetail @ChaluOrPujigatId,@OfficeId,@Message OUT", ChaluOrPujigatIdParam, OfficeIdParam, MessageParam);
                    returnModel.ReturnMessage = MessageParam.SqlValue.ToString();
                    returnModel.PrimaryId = model.ChaluPujigatId;

                }
                catch (Exception)
                {
                    returnModel.ReturnMessage = "Error Please try again";
                    returnModel.PrimaryId = model.ChaluPujigatId;

                }

                return returnModel;
            }

        }











    }
}