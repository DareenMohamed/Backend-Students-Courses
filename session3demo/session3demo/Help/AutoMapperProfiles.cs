using AutoMapper;
using session3demo.Help.Dtos;
using session3demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace session3demo.Help
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            //   .ForMember(dest => dest.summary, opt => opt.MapFrom(src => src.Name + " " + src.Gpa));
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<StudentCourse, StudentCourseDto>().ReverseMap();

        }
    }
}
