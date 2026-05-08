using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models.Setups
{
    public class LetterSetupViewModel
    {
        public SamparikshadLetterOfficeSetupViewModel ObjSamparikshadLetterOfficeSetupViewModel { get; set; }
        public List<SamparikshadLetterOfficeSetupViewModel> SamparikshadLetterOfficeSetupViewModelList { get; set; }


    }
    public class SamparikshadLetterOfficeSetupViewModel
    {

        public int SamparikshadLetterSetupId { get; set; }
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public bool SetupStatus { get; set; }
        public int SetupType { get; set; }
        public int BerujuTypeId { get; set; }
    }
}