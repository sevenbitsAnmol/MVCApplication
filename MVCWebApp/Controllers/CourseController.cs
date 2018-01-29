using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWebApp.Models;
using PagedList;

namespace MVCWebApp.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index(int? page)
        {
            List<Course> course = new List<Course>();
            using (var context = new StudentCourseContext1())
            {
                try
                {
                    course = context.Courses.OrderByDescending(x=>x.CourseId).ToList();
                }
                catch (Exception)
                {

                }
            }
            return View(course.ToPagedList(page ?? 1,10));
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
                using (var context = new StudentCourseContext1())
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