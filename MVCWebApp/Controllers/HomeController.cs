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
            using (var context=new StudentCourseContext())
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
        public JsonResult Add(Student student)
        {
            try
            {
                //List<Student> StudentList = new List<Student>();
                using (var context = new StudentCourseContext())
                {
                    context.Students.Add(student);
                    context.SaveChanges();
                    var StudentList = LoadStudentList();
                    return StudentList;
                }
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }

        //Load student records
        public JsonResult LoadStudentList()
        {
            List<Student> StudentList = new List<Student>();
            using (var context=new StudentCourseContext())
            {
                StudentList = context.Students.OrderByDescending(x => x.Id).ToList();
            }
            return Json(StudentList,JsonRequestBehavior.AllowGet);
        }

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
                using (var context = new StudentCourseContext())
                {
                    StudentRecord = context.Students.Where(x => x.Id == Id).ToList();
                }
                return Json(StudentRecord, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Update(Student student)
        {
            try
            {
                //Update the student details
                using (var context = new StudentCourseContext())
                {
                    Student studentRecord = context.Students.Where(x => x.Id == student.Id).SingleOrDefault();
                    if (studentRecord!=null)
                    {
                        studentRecord.Id = student.Id;
                        studentRecord.Name = student.Name;
                        studentRecord.CourseId = student.CourseId;
                        studentRecord.Address = student.Address;
                        studentRecord.Gender = student.Gender;
                        studentRecord.Phone = student.Phone;
                        //context.Entry(studentRecord).CurrentValues.SetValues(student);
                        context.SaveChanges();
                    }
                }
                //return the student list in Json format
                var StudentList = LoadStudentList();
                return StudentList;
            }
            catch (Exception ex)
            {
                return Json(ex.Message,JsonRequestBehavior.AllowGet);
            }   
        }
    }
}