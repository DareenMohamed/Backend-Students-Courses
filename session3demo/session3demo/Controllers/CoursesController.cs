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
    public class CoursesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly DemoContext _context;

        public List<Course> _courses { get; set; }

        public CoursesController(IMapper mapper, DemoContext context)
        {
            _mapper = mapper;
            _context = context;

        }
        // GET: api/<Courses>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var courses = await _context.Courses.ToListAsync();
                return Ok(_mapper.Map<List<CourseDto>>(courses));
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "An error happened while getting all courses" });
            }
        }

        // POST api/<Courses>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, new { Error = "Please enter valid data!" });
                }
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();

                var courses = await _context.Courses.ToListAsync();
                return Ok(_mapper.Map<List<CourseDto>>(courses));
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "An error occured while adding the course" });
            }
        }

    }
}
