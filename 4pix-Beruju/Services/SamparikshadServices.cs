using _4pix_Beruju.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Services
{
    public class SamparikshadServices
    {

        public List<ListBerujuForSamparikshadActionVM> sp_GetRequestForActionSamparikshan(int OfficeId, int InternalOrExternalBerujuId)
        {
            using (BerujuEntities db = new BerujuEntities())
            {
                List<ListBerujuForSamparikshadActionVM> ReturnList = new List<ListBerujuForSamparikshadActionVM>();
                var officeIdparam = new System.Data.SqlClient.SqlParameter { ParameterName = "@OfficeId", Value = OfficeId };
                ReturnList = db.Database.SqlQuery<ListBerujuForSamparikshadActionVM>("sp_GetRequestForActionSamparikshan {0}", OfficeId).ToList();
                return ReturnList;
            }
        }
    }
}