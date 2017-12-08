using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MVCWebApp.Models
{
    public class Course
    {
        public Course()
        {

        }

        public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
