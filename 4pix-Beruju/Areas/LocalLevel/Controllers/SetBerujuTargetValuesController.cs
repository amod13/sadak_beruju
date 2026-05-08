using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _4pix_Beruju.Models;

namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    [Authorize]
    public class SetBerujuTargetValuesController : Controller
    {
        private BerujuEntities db = new BerujuEntities();
        int CurrentUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();
        Guid CurrentLoginUserId = _4pix_Beruju.Areas.Admin.functions.GetCurrentUser();
        // GET: LocalLevel/SetBerujuTargetValues
        public ActionResult Index()
        {
            return View(db.SetBerujuTargetValue.Where(x => x.OfficeId == CurrentUserOfficeId).ToList());
        }

        // GET: LocalLevel/SetBerujuTargetValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetBerujuTargetValue setBerujuTargetValue = db.SetBerujuTargetValue.Find(id);
            if (setBerujuTargetValue == null)
            {
                return HttpNotFound();
            }
            return View(setBerujuTargetValue);
        }

        // GET: LocalLevel/SetBerujuTargetValues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocalLevel/SetBerujuTargetValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SetBerujuTargetValue setBerujuTargetValue)
        {
            if (ModelState.IsValid)
            {
                setBerujuTargetValue.OfficeId = CurrentUserOfficeId;
                setBerujuTargetValue.CreaatedDate = DateTime.Now;
                setBerujuTargetValue.CreatedBy = 1;
                setBerujuTargetValue.IstQuardTargetVal = 0;
                setBerujuTargetValue.IIndQuardTargetVal = 0;
                setBerujuTargetValue.IIIrdQuardTargetVal = 0;

                db.SetBerujuTargetValue.Add(setBerujuTargetValue);             


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(setBerujuTargetValue);
        }

        // GET: LocalLevel/SetBerujuTargetValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetBerujuTargetValue setBerujuTargetValue = db.SetBerujuTargetValue.Find(id);
            if (setBerujuTargetValue == null)
            {
                return HttpNotFound();
            }
            return View(setBerujuTargetValue);
        }

        // POST: LocalLevel/SetBerujuTargetValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BerujuTargetId,OfficeId,FiscalYearId,IstQuardTargetVal,IIndQuardTargetVal,IIIrdQuardTargetVal,CreaatedDate,CreatedBy")] SetBerujuTargetValue setBerujuTargetValue)
        {
            if (ModelState.IsValid)
            {
                setBerujuTargetValue.IstQuardTargetVal = 0;
                setBerujuTargetValue.IIndQuardTargetVal = 0;
                setBerujuTargetValue.IIIrdQuardTargetVal = 0;
                db.Entry(setBerujuTargetValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(setBerujuTargetValue);
        }

        // GET: LocalLevel/SetBerujuTargetValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetBerujuTargetValue setBerujuTargetValue = db.SetBerujuTargetValue.Find(id);
            if (setBerujuTargetValue == null)
            {
                return HttpNotFound();
            }
            return View(setBerujuTargetValue);
        }

        // POST: LocalLevel/SetBerujuTargetValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SetBerujuTargetValue setBerujuTargetValue = db.SetBerujuTargetValue.Find(id);
            db.SetBerujuTargetValue.Remove(setBerujuTargetValue);
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
