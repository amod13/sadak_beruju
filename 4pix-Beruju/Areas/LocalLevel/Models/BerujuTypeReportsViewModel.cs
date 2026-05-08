using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4pix_Beruju.Models;
namespace _4pix_Beruju.Areas.LocalLevel.Models
{
    public class BerujuTypeReportsViewModel
    {

        public int FiscalYearIdSearch { get; set; }
        public int InternalOrExternalIdSearch { get; set; }
        public int OfficeIdSearch { get; set; }

        public BerujuTypeModels objBerujuTypeModels { get; set; }


    }
    public class BerujuTypeModels
    {
        public SaidantikBeruju objSaidantikBerujuViewModel { get; set; }
        public List<SaidantikBeruju> SaidantikBerujuListViewModel;

        public BerujuNotDoneModel objBerujuNotDoneModel { get; set; }
        public List<BerujuNotDoneModel> BerujuNotDoneModelList { get; set; }
    }


}