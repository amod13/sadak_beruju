using _4pix_Beruju.Helpers;
using _4pix_Beruju.Models;
using _4pix_Beruju.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    public class ImageManagementController : Controller
    {
        private readonly BerujuEntities db = new BerujuEntities();

        ImageManagementService IMS = new ImageManagementService();

        // =====================================================
        // INDEX
        // =====================================================

        [OfficeTypeAuthorize(RequiredOfficeType = 2)]
        public ActionResult Index()
        {
            var list = db.BerujuFilesByDafa
                         .Include("Documents")
                         .OrderByDescending(x => x.Id)
                         .ToList();

            return View(list);
        }

        public ActionResult GetOfficeFiles()
        {
            int currentUserOfficeId =
                _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();

            var list = db.BerujuFilesByDafa
                         .Include("Documents")
                         .Where(x => x.OfficeId == currentUserOfficeId)
                         .OrderByDescending(x => x.Id)
                         .ToList();

            return View(list);   // reuse Index view
        }




        // =====================================================
        // CREATE (GET)
        // =====================================================
        [OfficeTypeAuthorize(RequiredOfficeType = 2)]
        public ActionResult Create()
        {
            return View(new BerujuFilesByDafaVM());
        }

        // =====================================================
        // CREATE (POST)
        // =====================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BerujuFilesByDafaVM model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var OfficeId = GetSelectedOfficeId(model);

            var entity = new BerujuFilesByDafa
            {
                OfficeId = OfficeId ?? 0,
                FiscalYearId = model.FiscalYearId,
                TotalBerujuDafa = model.TotalBerujuDafa,
                CreatedDate = DateTime.Now
            };

            db.BerujuFilesByDafa.Add(entity);
            db.SaveChanges();

            // 🔁 Reusable file save
            SaveBerujuFiles(entity.Id, model.Files);

            TempData["Success"] = "बेरुजु दफा सफलतापूर्वक थपियो ।";
            return RedirectToAction("Index");
        }


        [OfficeTypeAuthorize(RequiredOfficeType = 2)]
        public ActionResult ViewFiles(int id)
        {
            var data = db.BerujuFilesByDafa
                         .Include("Documents")
                         .FirstOrDefault(x => x.Id == id);

            if (data == null)
                return HttpNotFound();

            return View(data);
        }

        public ActionResult ViewOfficeFiles(int id)
        {
            int currentUserOfficeId =
                _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();

            var data = db.BerujuFilesByDafa
                         .Include("Documents")
                         .FirstOrDefault(x =>
                             x.Id == id &&
                             x.OfficeId == currentUserOfficeId);

            if (data == null)
                return new HttpStatusCodeResult(403); // Forbidden

            return View("ViewFiles", data); // reuse same view
        }

        // =====================================================
        // EDIT (GET)
        // =====================================================
        [OfficeTypeAuthorize(RequiredOfficeType = 2)]
        public ActionResult Edit(int id)
        {
            var entity = db.BerujuFilesByDafa
                           .Include("Documents")
                           .FirstOrDefault(x => x.Id == id);

            if (entity == null)
                return HttpNotFound();

            var vm = new BerujuFilesByDafaVM
            {
                Id = entity.Id,
                KaryalayaSearchId = entity.OfficeId,
                FiscalYearId = entity.FiscalYearId,
                TotalBerujuDafa = entity.TotalBerujuDafa,
                ExistingFiles = entity.Documents.ToList()
            };

            return View(vm);
        }

        // =====================================================
        // EDIT (POST)
        // =====================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BerujuFilesByDafaVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var OfficeId = GetSelectedOfficeId(model);
            var entity = db.BerujuFilesByDafa.Find(model.Id);
            if (entity == null)
                return HttpNotFound();

            entity.OfficeId = OfficeId  ?? 0;
            entity.FiscalYearId = model.FiscalYearId;
            entity.TotalBerujuDafa = model.TotalBerujuDafa;

            db.SaveChanges();

            // 🔁 Add new files (old remain)
            SaveBerujuFiles(entity.Id, model.Files);

            TempData["Success"] = "बेरुजु दफा सफलतापूर्वक अपडेट भयो ।";
            return RedirectToAction("Index");
        }

        // =====================================================
        // REUSABLE FILE SAVE FUNCTION
        // =====================================================
        private void SaveBerujuFiles(int berujuId, IEnumerable<HttpPostedFileBase> files)
        {
            if (files == null) return;

            string folderPath = "~/Uploads/BerujuFiles/";
            string physicalPath = Server.MapPath(folderPath);

            if (!Directory.Exists(physicalPath))
                Directory.CreateDirectory(physicalPath);

            foreach (var file in files)
            {
                if (file == null || file.ContentLength == 0)
                    continue;

                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string fullPath = Path.Combine(physicalPath, fileName);

                file.SaveAs(fullPath);

                db.BerujuFilesByDafaDocument.Add(
                    new BerujuFilesByDafaDocument
                    {
                        BerujuFilesByDafaId = berujuId,
                        FileName = Path.GetFileName(file.FileName),
                        FilePath = folderPath + fileName,
                        UploadedDate = DateTime.Now
                    });
            }
            db.SaveChanges();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [OfficeTypeAuthorize(RequiredOfficeType = 2)]
        public JsonResult Delete(int id)
        {
            var record = db.BerujuFilesByDafa
                           .Include("Documents")
                           .FirstOrDefault(x => x.Id == id);

            if (record == null)
                return Json(new { success = false, message = "Record not found" });

            // 1️⃣ Delete physical files
            if (record.Documents != null)
            {
                foreach (var doc in record.Documents)
                {
                    if (!string.IsNullOrEmpty(doc.FilePath))
                    {
                        string physicalPath = Server.MapPath(doc.FilePath);
                        if (System.IO.File.Exists(physicalPath))
                            System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // 2️⃣ Delete DB record (cascade deletes documents)
            db.BerujuFilesByDafa.Remove(record);
            db.SaveChanges();

            return Json(new { success = true });
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [OfficeTypeAuthorize(RequiredOfficeType = 2)]
        public JsonResult DeleteFile(int id)
        {
            var file = db.BerujuFilesByDafaDocument.Find(id);
            if (file == null)
                return Json(new { success = false });

            string physicalPath = Server.MapPath(file.FilePath);
            if (System.IO.File.Exists(physicalPath))
                System.IO.File.Delete(physicalPath);

            db.BerujuFilesByDafaDocument.Remove(file);
            db.SaveChanges();

            return Json(new { success = true });
        }


        private int? GetSelectedOfficeId(BerujuFilesByDafaVM filter)
        {
            switch (filter.OfficeTypeSearchId)
            {
                case 2: // Ministry
                    return filter.MininstrySearchId > 0
                        ? (int?)filter.MininstrySearchId
                        : (int?)null;

                case 3: // Division
                    return (filter.MininstrySearchId > 0 && filter.BivagSearchId > 0)
                        ? (int?)filter.BivagSearchId
                        : (int?)null;

                case 4: // Directorate
                    return (filter.MininstrySearchId > 0 &&
                            filter.BivagSearchId > 0 &&
                            filter.NirdeshnalayaSearchId > 0)
                        ? (int?)filter.NirdeshnalayaSearchId
                        : (int?)null;

                case 5: // Office
                    return (filter.MininstrySearchId > 0 &&
                            filter.BivagSearchId > 0 &&
                            filter.NirdeshnalayaSearchId > 0 &&
                            filter.KaryalayaSearchId > 0)
                        ? (int?)filter.KaryalayaSearchId
                        : (int?)null;

                default:
                    return filter.OfficeId; // already nullable
            }
        }


        public ActionResult ListFy()
        {
            return View(db.FiscalYearRecord.ToList().OrderBy(x=>x.FiscalYearTitle));
        }

        // tree



        //[OfficeTypeAuthorize(RequiredOfficeType = 2)]
        public ActionResult BrowseFiscalYearFiles(int fiscalYearId)
        {
            // IMPORTANT:
            // Replace "FiscalYears" and "FYCODE" with your actual table/column names.
            // Example assumes db.FiscalYears and property FYCODE.

            var fiscalYear = db.FiscalYearRecord.FirstOrDefault(x => x.FiscalYearId == fiscalYearId);
            if (fiscalYear == null)
                return HttpNotFound("Fiscal year not found.");

            string fyCode = fiscalYear.FiscalYearId.ToString(); // <-- change if your field name differs

            if (string.IsNullOrWhiteSpace(fyCode))
                return HttpNotFound("Fiscal year code not found.");

            string rootVirtualPath = $"~/Uploads/BerujuFiles/Raw/{fyCode}";
            string rootPhysicalPath = Server.MapPath(rootVirtualPath);

            var vm = new FiscalYearFileExplorerVM
            {
                FiscalYearId = fiscalYearId,
                FYCode = fyCode,
                RootVirtualPath = rootVirtualPath,
                FolderExists = Directory.Exists(rootPhysicalPath)
            };

            if (vm.FolderExists)
            {

                var rootDir = new DirectoryInfo(rootPhysicalPath);
                var naturalComparer = new NaturalStringComparer();


                // Instead of single RootNode, we pass multiple root nodes
                foreach (var dir in rootDir.GetDirectories().OrderBy(f => f.Name, naturalComparer))
                {
                    vm.RootNodes.Add(new FileTreeNodeVM
                    {
                        Name = dir.Name,
                        RelativePath = dir.Name,
                        IsDirectory = true,
                        Children = BuildTree(dir, rootPhysicalPath)
                    });
                }

                // Also include files directly under root
                foreach (var file in rootDir.GetFiles().OrderBy(f => f.Name, naturalComparer))
                {
                    vm.RootNodes.Add(new FileTreeNodeVM
                    {
                        Name = file.Name,
                        RelativePath = file.Name,
                        IsDirectory = false
                    });
                }
            }

            return View(vm);
        }


        public ActionResult ViewFile(int fiscalYearId, string relativePath)
        {
            var fiscalYear = db.FiscalYearRecord.FirstOrDefault(x => x.FiscalYearId == fiscalYearId);
            if (fiscalYear == null) return HttpNotFound();

            string fyCode = fiscalYear.FiscalYearId.ToString(); // or fiscalYear.FYCODE if available
            string rootPhysicalPath = Server.MapPath($"~/Uploads/BerujuFiles/Raw/{fyCode}");

            if (string.IsNullOrWhiteSpace(relativePath))
                return HttpNotFound();

            relativePath = relativePath.Replace("/", "\\").TrimStart('\\');

            string fullPath = Path.Combine(rootPhysicalPath, relativePath);

            if (!System.IO.File.Exists(fullPath))
                return HttpNotFound();

            string mimeType = MimeMapping.GetMimeMapping(fullPath);

            // inline = preview in browser if supported
            return File(fullPath, mimeType);
        }

        public ActionResult DownloadFile(int fiscalYearId, string relativePath)
        {
            var fiscalYear = db.FiscalYearRecord.FirstOrDefault(x => x.FiscalYearId == fiscalYearId);
            if (fiscalYear == null) return HttpNotFound();

            string fyCode = fiscalYear.FiscalYearId.ToString(); // or fiscalYear.FYCODE if available
            string rootPhysicalPath = Server.MapPath($"~/Uploads/BerujuFiles/Raw/{fyCode}");

            if (string.IsNullOrWhiteSpace(relativePath))
                return HttpNotFound();

            relativePath = relativePath.Replace("/", "\\").TrimStart('\\');

            string fullPath = Path.Combine(rootPhysicalPath, relativePath);

            if (!System.IO.File.Exists(fullPath))
                return HttpNotFound();

            string mimeType = MimeMapping.GetMimeMapping(fullPath);
            string fileName = Path.GetFileName(fullPath);

            return File(fullPath, mimeType, fileName); // forces download
        }

        //[OfficeTypeAuthorize(RequiredOfficeType = 2)]
        //public ActionResult DownloadFiscalYearFile(int fiscalYearId, string relativePath)
        //{
        //    if (string.IsNullOrWhiteSpace(relativePath))
        //        return new HttpStatusCodeResult(400, "Invalid file path.");

        //    var fiscalYear = db.FiscalYearRecord.FirstOrDefault(x => x.FiscalYearId == fiscalYearId);
        //    if (fiscalYear == null)
        //        return HttpNotFound("Fiscal year not found.");

        //    string fyCode = fiscalYear.FiscalYearId.ToString(); // <-- change if needed
        //    if (string.IsNullOrWhiteSpace(fyCode))
        //        return HttpNotFound("Fiscal year code not found.");

        //    string rootVirtualPath = $"~/Uploads/BerujuFiles/Raw/{fyCode}";
        //    string rootPhysicalPath = Server.MapPath(rootVirtualPath);

        //    if (!Directory.Exists(rootPhysicalPath))
        //        return HttpNotFound("Folder not found.");

        //    // Normalize requested file path
        //    relativePath = relativePath.Replace('/', Path.DirectorySeparatorChar)
        //                               .Replace('\\', Path.DirectorySeparatorChar);

        //    string fullPath = Path.GetFullPath(Path.Combine(rootPhysicalPath, relativePath));
        //    string rootFullPath = Path.GetFullPath(rootPhysicalPath);

        //    // Security check: file must remain inside fiscal year folder
        //    if (!fullPath.StartsWith(rootFullPath, System.StringComparison.OrdinalIgnoreCase))
        //        return new HttpStatusCodeResult(403, "Access denied.");

        //    if (!System.IO.File.Exists(fullPath))
        //        return HttpNotFound("File not found.");

        //    string fileName = Path.GetFileName(fullPath);
        //    string mimeType = MimeMapping.GetMimeMapping(fileName);

        //    return File(fullPath, mimeType, fileName);
        //}

        //helper
        private List<FileTreeNodeVM> BuildTree(DirectoryInfo directory, string rootPhysicalPath)
        {
            var nodes = new List<FileTreeNodeVM>();
            var naturalComparer = new NaturalStringComparer();
            // Folders first
            foreach (var dir in directory.GetDirectories().OrderBy(d => d.Name,naturalComparer))
            {
                string relativePath = GetRelativePath(rootPhysicalPath, dir.FullName);

                var dirNode = new FileTreeNodeVM
                {
                    Name = dir.Name,
                    RelativePath = relativePath,
                    IsDirectory = true,
                    Children = BuildTree(dir, rootPhysicalPath)
                };

                nodes.Add(dirNode);
            }

            // Files next
            foreach (var file in directory.GetFiles().OrderBy(f => f.Name, naturalComparer))
            {
                string relativePath = GetRelativePath(rootPhysicalPath, file.FullName);

                nodes.Add(new FileTreeNodeVM
                {
                    Name = file.Name,
                    RelativePath = relativePath,
                    IsDirectory = false
                });
            }

            return nodes;
        }

        private string GetRelativePath(string rootPath, string fullPath)
        {
            if (!rootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                rootPath += Path.DirectorySeparatorChar;

            var rootUri = new System.Uri(rootPath);
            var fullUri = new System.Uri(fullPath);

            string relative = System.Uri.UnescapeDataString(
                rootUri.MakeRelativeUri(fullUri).ToString()
            );

            return relative.Replace('/', Path.DirectorySeparatorChar);
        }

        //

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }



        public ActionResult LagatDocumentTree()
        {
            int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
            var model = IMS.GetLagatDocumentTree(CurrentUserOfficeId);
            return View(model);
        }
    }
}
