using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMsViaEntityFramework.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual IEnumerable<Enrollment> Enrollments { get; set; }
    }
}