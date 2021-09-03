using FluentValidation;
using session3demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace session3demo.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public string courseName { get; set; }
        public string studentName { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }

    }
}
public class StudentCourseValidator : AbstractValidator<StudentCourse>
{
    public StudentCourseValidator()
    {
       // RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.courseName).NotNull().Length(1, 50);
        RuleFor(x => x.studentName).NotNull().Length(1, 50);

    }
}
