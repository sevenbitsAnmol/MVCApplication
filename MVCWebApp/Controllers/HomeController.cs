using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWebApp.Models;
using PagedList;

namespace MVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        //Return student records
        public ActionResult Index(int? page)
        {
            List<Student> student = new List<Student>();
            using (var context=new StudentCourseContext1())
            {
                try
                {
                     student = context.Students.OrderByDescending(x=>x.Id).ToList();
                }
                catch (Exception)
                {
                    
                }
            }
            return View(student.ToPagedList(page ?? 1,10));
        }

        [HttpGet]
        public ActionResult Create()
        {
           return View();
        }
        
        //Insert a new student record
        [HttpPost]
        public void Add(Student student)
        {
            try
            {
                using (var context = new StudentCourseContext1())
                {
                    context.Students.Add(student);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                
            }
        }

        //Load student records
        public JsonResult LoadStudentList()
        {
            List<Student> StudentList = new List<Student>();
            using (var context=new StudentCourseContext1())
            {
                StudentList = context.Students.ToList();
            }
            return Json(StudentList,JsonRequestBehavior.AllowGet);
        }

        //Binding Courses name with DropDownList
        public JsonResult BindDropDownList()
        {
            List<Course> Courselist = new List<Course>();
            using (var context = new StudentCourseContext1())
            {
                Courselist = context.Courses.ToList();
            }
            return Json(Courselist, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        public JsonResult GetStudent(int Id)
        {
            if (Id==0)
            {
                return null;
            }
            else
            {
                List<Student> StudentRecord = new List<Student>();
                using (var context = new StudentCourseContext1())
                {
                    StudentRecord = context.Students.Where(x => x.Id == Id).ToList();
                }
                return Json(StudentRecord, JsonRequestBehavior.AllowGet);
            }
        }
    }
}