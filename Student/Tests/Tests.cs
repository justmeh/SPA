using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Student.Controllers;
using Xunit;
namespace Student.Tests
{

    public class Tests
    {
        [Fact]
        public void AddUserTest()
        {
            var appDataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Claudiu Sirbu\\source\\repos\\SE\\Student\\App_Data");

            AppDomain.CurrentDomain.SetData("DataDirectory", appDataDir);
            ManagementController managementController = new ManagementController();
            managementController.AddUser("stoe", "password", "stoenica", "robert", 1960718162021, "stoenica.robert@yahoo.com", "admin");
            Student.Models.ApplicationDbContext dbContext = Models.ApplicationDbContext.Create();
            List<Student.Models.User> list = dbContext.Users.Where(m => m.Username == "stoe").ToList();
            var count = list.Count;
            Assert.True(count == 1);
            var uid = list[0].ID;
            var role = dbContext.Roles.Where(r => r.UserID == uid).ToList()[0];
            dbContext.Roles.Remove(role);
            dbContext.Users.Remove(list[0]);
            dbContext.SaveChanges();
            dbContext.Dispose();
        }

        [Fact]
        public void TestUserUnique()
        {
            var appDataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Claudiu Sirbu\\source\\repos\\SE\\Student\\App_Data");

            AppDomain.CurrentDomain.SetData("DataDirectory", appDataDir);
            ManagementController managementController = new ManagementController();
            managementController.AddUser("stoe", "password", "stoenica", "robert", 1960718162021, "stoenica.robert@yahoo.com", "admin");
            managementController.AddUser("stoe", "password", "stoenica", "robert", 1960718162021, "stoenica.robert@yahoo.com", "admin");
            Student.Models.ApplicationDbContext dbContext = Models.ApplicationDbContext.Create();
            List<Student.Models.User> list = dbContext.Users.Where(m => m.Username == "stoe").ToList();
            var count = list.Count;
            Assert.Equal(count, 1);
            var uid = list[0].ID;
            var role = dbContext.Roles.Where(r => r.UserID == uid).ToList()[0];
            dbContext.Roles.Remove(role);
            dbContext.Users.Remove(list[0]);
            dbContext.SaveChanges();
            dbContext.Dispose();
        }

        [Fact]
        public void LoginFailedTest()
        {
            var appDataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Claudiu Sirbu\\source\\repos\\SE\\Student\\App_Data");

            AppDomain.CurrentDomain.SetData("DataDirectory", appDataDir);
            AccountController accountController = new AccountController();
            ManagementController managementController = new ManagementController();
            managementController.AddUser("stoe", "password", "stoenica", "robert", 1960718162021, "stoenica.robert@yahoo.com", "admin");
            var result = accountController.Login("stoe2", "password") as ViewResult;
            Assert.True(result != null);
            Student.Models.ApplicationDbContext dbContext = Models.ApplicationDbContext.Create();
            List<Student.Models.User> list = dbContext.Users.Where(m => m.Username == "stoe").ToList();
            var uid = list[0].ID;
            var role = dbContext.Roles.Where(r => r.UserID == uid).ToList()[0];
            dbContext.Roles.Remove(role);
            dbContext.Users.Remove(list[0]);
            dbContext.SaveChanges();
            dbContext.Dispose();
        }


        [Fact]
        public void DeleteUser()
        {
            var appDataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Claudiu Sirbu\\source\\repos\\SE\\Student\\App_Data");

            AppDomain.CurrentDomain.SetData("DataDirectory", appDataDir);
            ManagementController managementController = new ManagementController();
            managementController.AddUser("stoe", "password", "stoenica", "robert", 1960718162021, "stoenica.robert@yahoo.com", "admin");
            Student.Models.ApplicationDbContext dbContext = Models.ApplicationDbContext.Create();
            List<Student.Models.User> users = dbContext.Users.Where(m => m.Username == "stoe").ToList();
            var ID = users[0].ID;
            dbContext.Users.Remove(users[0]);
            dbContext.SaveChanges();
            List<Student.Models.User> list = dbContext.Users.Where(m => m.ID == ID).ToList();
            var count = list.Count;
            Assert.Equal(count, 0);
            if (list.Count != 0)
            {
                var uid = list[0].ID;
                var role = dbContext.Roles.Where(r => r.UserID == uid).ToList()[0];
                dbContext.Roles.Remove(role);
                dbContext.Users.Remove(list[0]);
                dbContext.SaveChanges();
            }
            dbContext.Dispose();
        }

        [Fact]
        public void AddProjectTest()
        {
            var appDataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Claudiu Sirbu\\source\\repos\\SE\\Student\\App_Data");

            AppDomain.CurrentDomain.SetData("DataDirectory", appDataDir);
            ManagementController managementController = new ManagementController();
            managementController.AddProject("project", false);
            Student.Models.ApplicationDbContext dbContext = Models.ApplicationDbContext.Create();
            List<Student.Models.Project> list = dbContext.Projects.Where(m => m.Name == "project").ToList();
            var count = list.Count;
            Assert.True(count != 0);
            dbContext.Projects.Remove(list[0]);
            dbContext.SaveChanges();
            dbContext.Dispose();
        }

        [Fact]
        public void DeleteProjectTest()
        {
            var appDataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Claudiu Sirbu\\source\\repos\\SE\\Student\\App_Data");

            AppDomain.CurrentDomain.SetData("DataDirectory", appDataDir);
            ManagementController managementController = new ManagementController();
            managementController.AddProject("project", false);
            Student.Models.ApplicationDbContext dbContext = Models.ApplicationDbContext.Create();
            List<Student.Models.Project> projects = dbContext.Projects.Where(m => m.Name == "project").ToList();
            var ID = projects[0].ID;
            dbContext.Projects.Remove(projects[0]);
            dbContext.SaveChanges();
            List<Student.Models.Project> list = dbContext.Projects.Where(m => m.ID == ID).ToList();
            var count = list.Count;
            Assert.Equal(count, 0);
            if (list.Count != 0)
            {
                dbContext.Projects.Remove(list[0]);
                dbContext.SaveChanges();
            }
            dbContext.Dispose();
        }

        [Fact]
        public void SetProjectTest()
        {
            var appDataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Claudiu Sirbu\\source\\repos\\SE\\Student\\App_Data");

            AppDomain.CurrentDomain.SetData("DataDirectory", appDataDir);
            ManagementController managementController = new ManagementController();
            managementController.AddUser("stoe", "password", "stoenica", "robert", 1960718162021, "stoenica.robert@yahoo.com", "admin");
            managementController.AddProject("project", false);
            managementController.SetProject("stoe", "project");
            Student.Models.ApplicationDbContext dbContext = Models.ApplicationDbContext.Create();
            dbContext.SaveChanges();
            List<Student.Models.User> user = dbContext.Users.Where(m => m.Username == "stoe").ToList();

            List<Student.Models.Project> project = dbContext.Projects.Where(m => m.Name == "project").ToList();

            var projectID = project[0].ID;
            var userID = user[0].ID;
            List<Student.Models.Users_Projects> projects = dbContext.UsersProjects.Where(m => m.ProjectID == projectID && m.UserID == userID).ToList();
            var firstProjectID = projects[0].ProjectID;
            var firstUserID = projects[0].UserID;
            Assert.Equal(firstProjectID, projectID);
            Assert.Equal(firstUserID, userID);
            dbContext.UsersProjects.Remove(projects[0]);
            dbContext.Projects.Remove(project[0]);
            var uid = user[0].ID;
            var role = dbContext.Roles.Where(r => r.UserID == uid).ToList()[0];
            dbContext.Roles.Remove(role);
            dbContext.Users.Remove(user[0]);
            dbContext.SaveChanges();
            dbContext.Dispose();
        }

        [Fact]
        public void ValidateEmailTest()
        {
            AccountController accountController = new AccountController();
            Assert.False(accountController.validateEmail("test@faraPunct"));
            Assert.False(accountController.validateEmail("test.fara@"));
            Assert.True(accountController.validateEmail("stoe.nica@yahoo.com"));
        }

        [Fact]
        public void ValidateCNPTest()
        {
            AccountController accountController = new AccountController();
            Assert.False(accountController.validateCNP(1999));
            Assert.True(accountController.validateCNP(1960718162021));
        }

        [Fact]
        public void MarkTest()
        {
            var appDataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\Claudiu Sirbu\\source\\repos\\SE\\Student\\App_Data");

            AppDomain.CurrentDomain.SetData("DataDirectory", appDataDir);
            ProjectController projectController = new ProjectController();
            ManagementController managementController = new ManagementController();
            managementController.AddUser("stoe1", "password", "stoenica", "robert", 1960718162021, "stoenica.robert@yahoo.com", "Student");
            managementController.AddProject("project2", false);
            managementController.SetProject("stoe1", "project2");
            Student.Models.ApplicationDbContext dbContext = Models.ApplicationDbContext.Create();
            dbContext.SaveChanges();
            List<Student.Models.User> user = dbContext.Users.Where(m => m.Username == "stoe1").ToList();

            List<Student.Models.Project> project = dbContext.Projects.Where(m => m.Name == "project2").ToList();

            var projectID = project[0].ID;
            var userID = user[0].ID;
            List<Student.Models.Users_Projects> projects = dbContext.UsersProjects.Where(m => m.ProjectID == projectID && m.UserID == userID).ToList();
            projectController.GiveMark(9, projects[0].UsersProjectsID);
            projects = dbContext.UsersProjects.Where(m => m.ProjectID == projectID && m.UserID == userID).ToList();
            var mark = projects[0].Mark + "";
            Assert.Equal("9", mark);
            dbContext.UsersProjects.Remove(projects[0]);
            dbContext.Projects.Remove(project[0]);
            var uid = user[0].ID;
            var role = dbContext.Roles.Where(r => r.UserID == uid).ToList()[0];
            dbContext.Roles.Remove(role);
            dbContext.Users.Remove(user[0]);
            dbContext.SaveChanges();
            dbContext.Dispose();
        }
    }
}