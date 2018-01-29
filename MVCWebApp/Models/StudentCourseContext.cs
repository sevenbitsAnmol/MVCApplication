using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCWebApp.Models
{
    public class StudentCourseContext1 : DbContext
    {
        public StudentCourseContext1():base()
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}