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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace session3demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DemoContext _context;

        public List<Student> _students { get; set; } 

        public StudentsController(IMapper mapper , DemoContext context)
        {
            _students = new List<Student>
        {
            new Student{Id = 1, Name = "mohammed", Gpa = 2.8},
            new Student{Id = 2, Name = "ahmed", Gpa = 2.6},
            new Student{Id = 3, Name = "rana", Gpa = 2.9},
            new Student{Id = 4, Name = "kamal", Gpa = 3.0},
            new Student{Id = 5, Name = "yasser", Gpa = 2.2},
            new Student{Id = 6, Name = "walid", Gpa = 3.1}
        };
            _mapper = mapper;
            _context = context;
        }
         // GET: api/<Students>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var students = await _context.Students.ToListAsync();
                return Ok(_mapper.Map<List<StudentDto>>(students));
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "An error happened while getting all students" });
            }
        }

        // GET api/<Students>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var student = await _context.Students.SingleOrDefaultAsync(st => st.Id == id);
                if (student == null) return NotFound(new { Error = "student with the given id doesn't exist" });
                return Ok(_mapper.Map<StudentDto>(student));

            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "an error happened while getting the student data" });
            }
        }

        // POST api/<Students>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            try
            {
                // validte student, if not valid return BadRequest, 400
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, new { Error = "Please enter valid data!" });
                }
               /* var students = await _context.Students.ToListAsync();
                for (int i = 0; i<students.Count;i++) {
                    if (students[i].Name == student.Name) {
                        return StatusCode(400, new { Error = "Student with this name already exists!" });
                    }
                }
               */
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();

                var students = await _context.Students.ToListAsync();
                return Ok(_mapper.Map<List<StudentDto>>(students));
         }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "an error happened while adding student" });
            }
        }

        // PUT api/<Students>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student student)
        {
            try
            {
                // validte student, if not valid return BadRequest, 400
                var localStudent = _students.SingleOrDefault(st => st.Id == id);
                if (localStudent == null) return NotFound(new { Error = "student with the given id doesn't exist" });
                localStudent.Gpa = student.Gpa;
                localStudent.Name = student.Name;
                return Ok(localStudent);

            }
            catch (Exception)
            {

                return StatusCode(500, new { Error = "an error happened while updating student data" });
            }

        }

        // DELETE api/<Students>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var localStudent = _students.SingleOrDefault(st => st.Id == id);
            if (localStudent == null) return NotFound(new { Error = "student with the given id doesn't exist" });
            _students.Remove(localStudent);
            return Ok(_students);

        }
    }
}
