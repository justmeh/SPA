using Student.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            ViewBag.message = "";

            return View();
        }

        public ActionResult Profile()
        {

            return View();
        }

        public ActionResult Grades()
        {

            return View();
        }

        public ActionResult Settings()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Student.Models.ApplicationDbContext dbContext = Student.Models.ApplicationDbContext.Create();

            Student.Models.User userModel = new Student.Models.User();

            List<Student.Models.User> list = dbContext.Users.Where(m => m.Username == username && m.Password == password).ToList();

            if (list.Count() != 0)
            {
                var uid = list[0].ID;

                var role = dbContext.Roles.Where(r => r.UserID == uid).ToList();

                dbContext.Dispose();

                Session.Timeout = 60;
                Session["Logged"] = true;
                Session["Name"] = list[0].FirstName + " " + list[0].LastName;
                Session["ID"] = list[0].ID;
                switch (role[0].Role)
                {
                    case Student.Models.UserRole.roles.student:
                        Session["Role"] = "student";
                        break;
                    case Student.Models.UserRole.roles.professor:
                        Session["Role"] = "prof";
                        break;
                    case Student.Models.UserRole.roles.administrator:
                        Session["Role"] = "admin";
                        break;
                }
                ViewBag.message = "";

                return RedirectToAction("Index", "Project");
            }
            else
                ViewBag.message = "Wrong username or password!";

            return View("Login");
        }

        public ActionResult Logout()
        {

            Session["Logged"] = false;
            Session["Name"] = "";
            Session["ID"] = null;
            Session["Role"] = "";

            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            Student.Models.ApplicationDbContext db = Student.Models.ApplicationDbContext.Create();

            var uid = (int)Session["ID"];

            var edit = db.Users.SingleOrDefault(u => u.ID == uid);

            if(edit.Password != model.OldPassword)
            {
                ViewBag.passerror = "Incorrect password.";
            }
            else
            {
                if(model.NewPassword == model.ConfirmPassword)
                {
                    edit.Password = model.NewPassword;

                    db.SaveChanges();
                    
                    ViewBag.passerror = "";

                    return RedirectToAction("Settings");
                }
                else
                {
                    ViewBag.passerror = "The new password and confirmation password do not match.";
                }
            }

            db.Dispose();
            return View("Settings",model);
        }

        [HttpPost]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            bool err = false;

            if(!validateCNP(model.CNP))
            {
                ViewBag.cnperror = "Invalid CNP.";
                err = true;
            }

            if (!validateEmail(model.Email))
            {
                ViewBag.emailerror = "Invalid email.";
                err = true;
            }

            if(err)
            {
                return View("Profile", model);
            }
            else
            {
                Student.Models.ApplicationDbContext db = Student.Models.ApplicationDbContext.Create();

                var uid = (int)Session["ID"];

                var edit = db.Users.SingleOrDefault(u => u.ID == uid);

                edit.FirstName = model.FirstName;
                edit.LastName = model.LastName;
                edit.CNP = model.CNP;
                edit.Email = model.Email;

                db.SaveChanges();
                db.Dispose();

                return RedirectToAction("Profile");
            }
            
        }

        public bool validateCNP(long cnp)
        {
            return !(cnp < 1000101010010 || cnp > 8991231529999);
        }

        public bool validateEmail(string email)
        {
            var atindex = email.IndexOf('@');
            var dotindex = email.LastIndexOf('.');

            return (atindex != -1 && dotindex != -1 && atindex == email.LastIndexOf('@') && atindex < dotindex && atindex != 0 && dotindex != (email.Length - 1));
        }
    }
}