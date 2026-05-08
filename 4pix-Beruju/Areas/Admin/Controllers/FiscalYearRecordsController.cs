using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models;
using _4pix_Beruju.Models.Setups;

namespace _4pix_Beruju.Areas.Admin.Controllers
{
    [Authorize]
    public class FiscalYearRecordsController : Controller
    {
        private BerujuEntities db = new BerujuEntities();

        // GET: Admin/FiscalYearRecords
        public ActionResult Index()
        {
            return View(db.FiscalYearRecord.ToList());
        }

        // GET: Admin/FiscalYearRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiscalYearRecord fiscalYearRecord = db.FiscalYearRecord.Find(id);
            if (fiscalYearRecord == null)
            {
                return HttpNotFound();
            }
            return View(fiscalYearRecord);
        }

        // GET: Admin/FiscalYearRecords/Create
        public ActionResult Create()
        {

            FiscalYearRecord model = new FiscalYearRecord();
            model.DateFromStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(DateTime.Now);
            model.DateToStr = _4pix_Beruju.Utilities.GetNepaliDateFromEng(DateTime.Now);
            return View(model);
        }

        // POST: Admin/FiscalYearRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create(FiscalYearRecord fiscalYearRecord)
        {
            if (ModelState.IsValid)
            {
                fiscalYearRecord.StartFrom = _4pix_Beruju.Utilities.GetEnglishDateFromNP(fiscalYearRecord.DateFromStr);
                fiscalYearRecord.EndDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(fiscalYearRecord.DateToStr);
                fiscalYearRecord.DisplayOrder = 5;
                fiscalYearRecord.PreFiscalYearId = 5;
                fiscalYearRecord.IsCurrent = false;
                db.FiscalYearRecord.Add(fiscalYearRecord);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fiscalYearRecord);
        }

        // GET: Admin/FiscalYearRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiscalYearRecord fiscalYearRecord = db.FiscalYearRecord.Find(id);
            if (fiscalYearRecord == null)
            {
                return HttpNotFound();
            }
            fiscalYearRecord.DateFromStr = Utilities.GetNepaliDateFromEng(fiscalYearRecord.StartFrom);
            fiscalYearRecord.DateToStr = Utilities.GetNepaliDateFromEng(fiscalYearRecord.EndDate);
            return View(fiscalYearRecord);
        }

        // POST: Admin/FiscalYearRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit(FiscalYearRecord fiscalYearRecord)
        {
            if (ModelState.IsValid)
            {
                fiscalYearRecord.StartFrom = _4pix_Beruju.Utilities.GetEnglishDateFromNP(fiscalYearRecord.DateFromStr);
                fiscalYearRecord.EndDate = _4pix_Beruju.Utilities.GetEnglishDateFromNP(fiscalYearRecord.DateToStr);
                fiscalYearRecord.DisplayOrder = 5;
                fiscalYearRecord.PreFiscalYearId = 5;
                db.Entry(fiscalYearRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fiscalYearRecord);
        }

        // GET: Admin/FiscalYearRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiscalYearRecord fiscalYearRecord = db.FiscalYearRecord.Find(id);
            if (fiscalYearRecord == null)
            {
                return HttpNotFound();
            }
            return View(fiscalYearRecord);
        }

        // POST: Admin/FiscalYearRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FiscalYearRecord fiscalYearRecord = db.FiscalYearRecord.Find(id);
            db.FiscalYearRecord.Remove(fiscalYearRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
