﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        //Return student records
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
        
        //Insert a new student record
        [HttpPost]
        public void Add(Student student)
        {
            try
            {
                using (var context = new StudentCourseContext())
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
            using (var context=new StudentCourseContext())
            {
                StudentList = context.Students.ToList();
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
    }
}