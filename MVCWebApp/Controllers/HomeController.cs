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

        [HttpGet]
        public ActionResult Create()
        {
           return View();
        }
        
        [HttpGet]
        public ActionResult Add(string Name,string Address,string Phone,string Gender,string Course)
        {
            try
            {
                using (var context = new StudentCourseContext())
                {
                    Student student = new Student();
                    student.Name = Name;
                    student.Address = Address;
                    student.Phone = Phone;
                    student.Gender = Gender;
                    student.CourseId = GetCourseId(Course);
                    context.Students.Add(student);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }

        public int GetCourseId(string Course)
        {
            using (var context=new StudentCourseContext())
            {
                int Id;
                Id = context.Courses.Where(x => x.Name == Course).Select(x => x.CourseId).First();
                if (Id < 0)
                {
                    Id = 0;
                }
                return Id;
            }
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
    }
}