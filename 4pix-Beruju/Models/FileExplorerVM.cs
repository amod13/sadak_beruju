using System.Collections.Generic;

namespace _4pix_Beruju.Models
{
    public class FileTreeNodeVM
    {
        public string Name { get; set; }
        public string RelativePath { get; set; }   // path relative to FY root
        public bool IsDirectory { get; set; }
        public List<FileTreeNodeVM> Children { get; set; } = new List<FileTreeNodeVM>();
          public FileSystemNodeVM RootNode { get; set; }
    }

    public class FiscalYearFileBrowserVM
    {
        public int FiscalYearId { get; set; }
        public string FiscalYearCode { get; set; }
        public FileSystemNodeVM RootNode { get; set; }
        public List<FileTreeNodeVM> RootNodes { get; set; } = new List<FileTreeNodeVM>();
    
    }

    public class FiscalYearFileExplorerVM
    {
        public int FiscalYearId { get; set; }
        public string FYCode { get; set; }
        public string RootVirtualPath { get; set; }
        public bool FolderExists { get; set; }
        public FileTreeNodeVM RootNode { get; set; }

        public List<FileTreeNodeVM> RootNodes { get; set; } = new List<FileTreeNodeVM>();


    }

    public class FileSystemNodeVM
    {
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public bool IsDirectory { get; set; }
        public List<FileSystemNodeVM> Children { get; set; } = new List<FileSystemNodeVM>();
    }



    public class OfficeTreeViewModel
    {
        public string OfficeName { get; set; }
        public string OfficeCode { get; set; }

        public List<FiscalYearNode> FiscalYears { get; set; }
    }

    public class FiscalYearNode
    {
        public string FiscalYearTitle { get; set; }

        public List<FileNode> Files { get; set; }
    }

    public class FileNode
    {
        public int ExternalBerujuId { get; set; }
        public string FilePath { get; set; }
    }

    public class FlatDocumentVM
    {
        public string OfficeName { get; set; }
        public string OfficeCode { get; set; }
        public string UploadFileDetailspath { get; set; }
        public int ExternalBerujuId { get; set; }
        public string FiscalYearTitle { get; set; }
    }
}