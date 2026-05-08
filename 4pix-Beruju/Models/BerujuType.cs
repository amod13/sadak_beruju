using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class BerujuType
    {

        public int BerujuTypeId { get; set; }
        public string TypeName { get; set; }
        public bool BerujuStatus { get; set; }

        public List<BerujuType> BerujuTypeList { get; set; }
    }

    public class BerujuSubType
    {
        public int BerujuSubTitleId { get; set; }
    
        public string SubTitle { get; set; }

        public int BerujuTypeId { get; set; }


        public string BerujuTypeName { get; set; }


        public bool SubTitleStatus { get; set; }

        public int OfficeId { get; set; }

        public List<BerujuSubType> BerujuSubTypeList { get; set; }
    }

    public class BerujuSubTypeChild
    {
        public int BerujuSubTitleChildId { get; set; }

        public int BerujuSubTitleId { get; set; }

        public string SubTitleChild { get; set; }

        public int BerujuTypeId { get; set; }

        public string BerujuSubTypeName { get; set; }

        public string SubTitle { get; set; }
        public string BerujuTypeName { get; set; }

        public bool SubTitleChildStatus { get; set; }

        public int OfficeId { get; set; }

        public List<BerujuSubTypeChild> BerujuSubTypeChildList { get; set; }
    }



}