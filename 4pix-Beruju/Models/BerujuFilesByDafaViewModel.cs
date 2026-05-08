using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace _4pix_Beruju.Models
{
    public class BerujuFilesByDafaVM
{
        public int Id { get; set; }

        public int OfficeTypeSearchId { get; set; }

        public int MininstrySearchId { get; set; }

        public int BivagSearchId { get; set; }

        public bool BerujuStatus { get; set; }

        public int NirdeshnalayaSearchId { get; set; }

        public int OfficeId { get; set; }

        public int KaryalayaSearchId { get; set; }


        public int FiscalYearId { get; set; }
        public int TotalBerujuDafa { get; set; }

        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        public List<BerujuFilesByDafaDocument> ExistingFiles { get; set; }
    }


public class BerujuFilesByDafa
{
        [Key]
    public int Id { get; set; }

    public int OfficeId { get; set; }
    public int FiscalYearId { get; set; }
    public int TotalBerujuDafa { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<BerujuFilesByDafaDocument> Documents { get; set; }
}


public class BerujuFilesByDafaDocument
{
        [Key]
    public int DocumentId { get; set; }

    public int BerujuFilesByDafaId { get; set; }

    public string FileName { get; set; }
    public string FilePath { get; set; }

    public DateTime UploadedDate { get; set; }

    public virtual BerujuFilesByDafa BerujuFilesByDafa { get; set; }
}

}