using FluentValidation;
using session3demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace session3demo.Models
{
    public class Course
    {
        public int id { get; set; }
        public string Name { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }

}
public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        RuleFor(x => x.id).NotNull();
        RuleFor(x => x.Name).NotNull().Length(1, 50);
    }
}
