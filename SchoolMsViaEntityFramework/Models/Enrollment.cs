using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMsViaEntityFramework.Models
{
    public enum Grade
    {
        A,
        B,
        C,
        D,
        E,
        F
    }

    public class Enrollment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Grade? Grade { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}