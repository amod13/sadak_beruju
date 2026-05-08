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
    public class ImageManagementService
    {


        public List<OfficeTreeViewModel> GetLagatDocumentTree(int officeId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                try
                {
                    var param = new SqlParameter("@OfficeId", officeId);

                    var result = db.Database
                        .SqlQuery<FlatDocumentVM>("EXEC dbo.GetLagatDocumentTreeByOffice @OfficeId", param)
                        .ToList();

                    var data = result
                        .GroupBy(x => new { x.OfficeName, x.OfficeCode })
                        .Select(o => new OfficeTreeViewModel
                        {
                            OfficeName = o.Key.OfficeName,
                            OfficeCode = o.Key.OfficeCode,
                            FiscalYears = o.GroupBy(f => f.FiscalYearTitle)
                                .Select(fy => new FiscalYearNode
                                {
                                    FiscalYearTitle = fy.Key,
                                    Files = fy.Select(f => new FileNode
                                    {
                                        ExternalBerujuId = f.ExternalBerujuId,
                                        FilePath = f.UploadFileDetailspath
                                    }).ToList()
                                }).ToList()
                        }).ToList();

                    return data;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}