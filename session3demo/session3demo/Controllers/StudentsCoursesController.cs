using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using session3demo.Data;
using session3demo.Help.Dtos;
using session3demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace session3demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsCoursesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly DemoContext _context;

        public List<StudentCourse> _studentsCourses { get; set; }

        public StudentsCoursesController(IMapper mapper , DemoContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        // GET: api/<StudentsCourses>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var studentscourses = await _context.StudentsCourses.ToListAsync();
                return Ok(_mapper.Map<List<StudentCourseDto>>(studentscourses));
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "An error happened while getting all enrollements" });
            }
        }

        // POST api/<StudentsCourses>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentCourse studentCourse)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, new { Error = "Please enter valid data!" });
                }
                var studentCourses = await _context.StudentsCourses.ToListAsync();
                for (int i = 0; i< studentCourses.Count;i++) {
                    if (studentCourses[i].studentName == studentCourse.studentName && studentCourses[i].courseName == studentCourse.courseName) {
                        return StatusCode(400, new { Error = "Student with this name already exists!" });
                    }
                }
               
                await _context.StudentsCourses.AddAsync(studentCourse);
                await _context.SaveChangesAsync();

                studentCourses = await _context.StudentsCourses.ToListAsync();
                return Ok(_mapper.Map<List<StudentCourseDto>>(studentCourses));
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "An error occured while enrolling student to course" });
            }
        }

    }
}
