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

namespace _4pix_Beruju.Areas.LocalLevel.Controllers
{
    [Authorize]
    public class CurrentOfficeChiefDetailsController : Controller
    {
        private BerujuEntities db = new BerujuEntities();

        int CurrentLoginUserOfficeId = _4pix_Beruju.Areas.Admin.functions.GetCurrentLoginUserClientId();

        // GET: LocalLevel/CurrentOfficeChiefDetails
        public ActionResult Index()
        {
            return View(db.CurrentOfficeChiefDetails.Where(x=>x.OfficeId==CurrentLoginUserOfficeId).ToList());
        }

        // GET: LocalLevel/CurrentOfficeChiefDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentOfficeChiefDetails currentOfficeChiefDetails = db.CurrentOfficeChiefDetails.Find(id);
            if (currentOfficeChiefDetails == null)
            {
                return HttpNotFound();
            }
            return View(currentOfficeChiefDetails);
        }

        // GET: LocalLevel/CurrentOfficeChiefDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocalLevel/CurrentOfficeChiefDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CurrentOfficeChiefDetails currentOfficeChiefDetails)
        {
            if (ModelState.IsValid)
            {
                currentOfficeChiefDetails.OfficeId = CurrentLoginUserOfficeId;
                currentOfficeChiefDetails.EmployeeStatus = true;
                db.CurrentOfficeChiefDetails.Add(currentOfficeChiefDetails);                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currentOfficeChiefDetails);
        }

        // GET: LocalLevel/CurrentOfficeChiefDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentOfficeChiefDetails currentOfficeChiefDetails = db.CurrentOfficeChiefDetails.Find(id);
            if (currentOfficeChiefDetails == null)
            {
                return HttpNotFound();
            }
            return View(currentOfficeChiefDetails);
        }

        // POST: LocalLevel/CurrentOfficeChiefDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CurrentOfficeChiefDetails currentOfficeChiefDetails)
        {
            if (ModelState.IsValid)
            {
                currentOfficeChiefDetails.OfficeId = CurrentLoginUserOfficeId;
                db.Entry(currentOfficeChiefDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currentOfficeChiefDetails);
        }

        // GET: LocalLevel/CurrentOfficeChiefDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentOfficeChiefDetails currentOfficeChiefDetails = db.CurrentOfficeChiefDetails.Find(id);
            if (currentOfficeChiefDetails == null)
            {
                return HttpNotFound();
            }
            return View(currentOfficeChiefDetails);
        }

        // POST: LocalLevel/CurrentOfficeChiefDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrentOfficeChiefDetails currentOfficeChiefDetails = db.CurrentOfficeChiefDetails.Find(id);
            db.CurrentOfficeChiefDetails.Remove(currentOfficeChiefDetails);
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
