using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewProject(int ID)
        {

            ViewBag.ID = ID;

            return View("_ViewProject");

        }

        [HttpPost]
        public ActionResult SaveInformation(string description, DateTime deadline, int ID)
        {
            Student.Models.ApplicationDbContext dbContext = Student.Models.ApplicationDbContext.Create();

            var project = dbContext.Projects.SingleOrDefault(m => m.ID == ID);

            project.Description = description;
            project.Deadline = deadline;

            dbContext.SaveChanges();

            dbContext.Dispose();

            ViewBag.ID = ID;
            return View("_ViewProject");
        }


        public ActionResult ViewAssigment(int ID)
        {
            ViewBag.ID = ID;
            return View("_ViewAssigment");
        }

        [HttpPost]
        public ActionResult SubmitComment(string comment, int userID, int ID)
        {
            Student.Models.ApplicationDbContext dbContext = Student.Models.ApplicationDbContext.Create();

            Student.Models.Users_Projects_Comments conversation = new Student.Models.Users_Projects_Comments();

            conversation.Comment = comment;
            conversation.UserID = userID;
            conversation.UsersProjectsID = ID;

            dbContext.UsersProjectsComments.Add(conversation);

            dbContext.SaveChanges();

            dbContext.Dispose();

            ViewBag.ID = ID;
            return View("_ViewAssigment");
        }

        [HttpPost]
        public ActionResult GiveMark(int mark, int ID)
        {
            Student.Models.ApplicationDbContext db = Student.Models.ApplicationDbContext.Create();

            var uprj = db.UsersProjects.SingleOrDefault(up => up.UsersProjectsID == ID);

            uprj.Mark = mark;

            db.SaveChanges();
            db.Dispose();

            ViewBag.ID = ID;
            return View("_ViewAssigment");
        }
        
    }

}