using FluentValidation;
using session3demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace session3demo.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Gpa { get; set; }
        public ICollection<StudentCourse> Courses { get; set; }

    }
}
public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotNull().Length(1, 50);
        RuleFor(x => x.Gpa).NotNull();
    }
}