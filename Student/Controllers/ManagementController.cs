using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        public ActionResult Index()
        {

            ViewBag.Error = TempData["Error"];

            return View();
        }

        [HttpPost]
        public ActionResult AddUser(string username, string password, string firstName, string lastName, long cnp, string email, string role)
        {

            Student.Models.ApplicationDbContext dbContext = Student.Models.ApplicationDbContext.Create();

            Student.Models.User userModel = new Student.Models.User();
            Student.Models.UserRole roleModel = new Student.Models.UserRole();

            if(dbContext.Users.Where(m => m.Username == username).ToList().Count > 0)
            {
                TempData["Error"] = "Username already exists!";
                return RedirectToAction("Index");
            }

            List<Student.Models.User> list = dbContext.Users.ToList();


            userModel.Username = username;
            userModel.Password = password;
            userModel.FirstName = firstName;
            userModel.LastName = lastName;
            userModel.CNP = cnp;
            userModel.Email = email;

            roleModel.UserID = userModel.ID;

            if (role == "Student")
                roleModel.Role = Student.Models.UserRole.roles.student;
            else if (role == "Professor")
                roleModel.Role = Student.Models.UserRole.roles.professor;
            else
                roleModel.Role = Student.Models.UserRole.roles.administrator;


            dbContext.Users.Add(userModel);
            dbContext.Roles.Add(roleModel);

            dbContext.SaveChanges();

            dbContext.Dispose();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteUser(string username)
        {

            Student.Models.ApplicationDbContext dbContext = Student.Models.ApplicationDbContext.Create();

            List<Student.Models.User> user = dbContext.Users.Where(m => m.Username == username).ToList();


            if (user.Count() != 0)
            {
                int id = user[0].ID;
                List<Student.Models.UserRole> role = dbContext.Roles.Where(m => m.UserID == id).ToList();




                dbContext.Users.Remove(user[0]);
                dbContext.Roles.Remove(role[0]);

                dbContext.SaveChanges();

                dbContext.Dispose();

            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddProject(string projectName, bool? projectOptional)
        {

            Student.Models.ApplicationDbContext dbContext = Student.Models.ApplicationDbContext.Create();

            Student.Models.Project projectModel = new Student.Models.Project();

            if (dbContext.Projects.Where(m => m.Name == projectName).ToList().Count > 0)
            {
                TempData["Error"] = "Project already exists!";
                return RedirectToAction("Index");
            }

            projectModel.Name = projectName;
            projectModel.Description = "";
            projectModel.Deadline = DateTime.Now;
            if (projectOptional == true)
                projectModel.Optional = true;
            else
                projectOptional = false;

            dbContext.Projects.Add(projectModel);

            dbContext.SaveChanges();

            dbContext.Dispose();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteProject(string projectName)
        {

            Student.Models.ApplicationDbContext dbContext = Student.Models.ApplicationDbContext.Create();

            List<Student.Models.Project> project = dbContext.Projects.Where(m => m.Name == projectName).ToList();


            if (project.Count() != 0)
            {

                dbContext.Projects.Remove(project[0]);

                dbContext.SaveChanges();

                dbContext.Dispose();
            }



            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SetProject(string username, string projectName)
        {

            Student.Models.ApplicationDbContext dbContext = Student.Models.ApplicationDbContext.Create();

            List<Student.Models.User> user = dbContext.Users.Where(m => m.Username == username).ToList();

            List<Student.Models.Project> project = dbContext.Projects.Where(m => m.Name == projectName).ToList();


            Student.Models.Users_Projects users_projects = new Student.Models.Users_Projects();

            if (user.Count() != 0 && project.Count() != 0)
            {

                users_projects.UserID = user[0].ID;
                users_projects.ProjectID = project[0].ID;
                users_projects.Mark = 0;


                dbContext.UsersProjects.Add(users_projects);

                dbContext.SaveChanges();


            }


            dbContext.Dispose();

            return RedirectToAction("Index");
        }
    }
}