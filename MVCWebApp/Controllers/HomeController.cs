using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Student> student = new List<Student>();
            using (var context=new StudentCourseContext())
            {
                try
                {
                     student = context.Students.ToList();
                }
                catch (Exception)
                {
                    
                }
            }
            return View(student.ToList());
        }

        public ActionResult Create()
        {
           return View();
        }

        //[HttpPost]
        /*public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var context = new StudentCourseContext())
                    {
                        student.CourseId = 0;
                        context.Students.Add(student);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    
                }
            }
            return View("Create");
        }*/

        //Binding Courses name with DropDownList
        public JsonResult BindDropDownList()
        {
            List<Course> Courselist = new List<Course>();
            using (var context = new StudentCourseContext())
            {
                Courselist = context.Courses.ToList();
            }
            return Json(Courselist, JsonRequestBehavior.AllowGet);
        }
    }
}