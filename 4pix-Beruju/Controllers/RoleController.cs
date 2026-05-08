using _4pix_Beruju.Areas.Admin.Models;
using _4pix_Beruju.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4pix_Beruju.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext db;
        // GET: Role
        public RoleController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }
        public ActionResult Create()
        {
            var roles = new IdentityRole();
            return View(roles);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            db.Roles.Add(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddRoleToUser()
        {
            ViewBag.Name = new SelectList(db.Roles.ToList(), "Name", "Name");
            ViewBag.Id = new SelectList(db.Users.ToList(), "Id", "Email");
            UsersRoleViewModel model = new UsersRoleViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddRoleToUser(UsersRoleViewModel model)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            userManager.AddToRoles(model.Id, model.Name);
            return RedirectToAction("Index");

        }










    }
}