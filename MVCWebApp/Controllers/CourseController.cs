using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            List<Course> course = new List<Course>();
            using (var context = new StudentCourseContext())
            {
                try
                {
                    course = context.Courses.ToList();
                }
                catch (Exception)
                {

                }
            }
            return View(course.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                using (var context = new StudentCourseContext())
                {
                    context.Courses.Add(course);
                    context.SaveChanges();
                }
                List<Course> Courselist = new List<Course>();
                Courselist.Add(course);
                return View("Create");
            }
            return View("Create");
        }
    }
}